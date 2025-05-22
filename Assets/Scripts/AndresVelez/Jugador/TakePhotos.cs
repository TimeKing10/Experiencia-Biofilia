using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor;

public class TakePhotos : MonoBehaviour
{
    public GameObject floatingPointsPrefab; 

    private int pointsPerPhoto;
    private static int nextID = 1;
    public Image fondoJugador; // Asigna esta en el Inspector o con GetComponent

    public int jugadorID;
    public Camera photoCamera;
    public LayerMask animalLayer;
    public RectTransform apuntador;

    public TextMeshProUGUI photosRemainingText;
    public Image previewImage;
    public CanvasGroup previewCanvasGroup;

    public AudioSource cameraSound;
    public static int totalScore = 0;
    public TextMeshProUGUI scoreText;
    public Canvas uiCanvas;
    public TextMeshProUGUI playerNameText;

    public static int fotosBuenas;
    public static int fotosEpicas;
    public static int fotosDesen;

    public int maxPhotos = 7;
    public float photoCooldown = 1.5f;
    public float rechargeTime = 1f;
    public float previewDuration = 2f;
    public int fotoSize = 200;
    public float rangoRaycast = 30f;

    public GameObject MiraDefault;
    public GameObject MiraMala;
    public GameObject MiraBuena;
    public GameObject MiraExcelente;

    private int photosRemaining;
    private bool canTakePhoto = true;
    private bool isReloading = false;

    private void Awake()
    {
        jugadorID = nextID;
        nextID++;

        var cargador = transform.Find("CargadorJugador1");
        if (cargador != null){playerNameText = cargador.GetComponent<TextMeshProUGUI>();}
        


    }

    void Start()
    {
        Referencias.Instance.AsignarValores(this);

        photosRemaining = maxPhotos;
        Invoke(nameof(SafeUpdateUI), 0.1f); // Espera una décima de segundo
        ActivarMira(MiraDefault);

        if (playerNameText != null)
        {
            playerNameText.text = "Player " + jugadorID;
        }

        if (jugadorID == 2 && fondoJugador != null)
        {
            fondoJugador.color = new Color(1f, 0.7f, 0.2f);
        }

    }

    void SafeUpdateUI()
    {
        if (playerNameText != null)
        {
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("playerNameText aún es null en SafeUpdateUI");
        }
    }

    public static void ResetPlayer()
    {
        nextID = 1;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && canTakePhoto)
        {
            if (photosRemaining > 0)
            {
                StartCoroutine(TakePhoto());
            }
        }
    }

    IEnumerator TakePhoto()
    {
        canTakePhoto = false;
        photosRemaining--;
        ActivarMira(MiraMala);       
        Animal animal = DetectarAnimal();

        if (animal != null)
        {
            pointsPerPhoto = animal.GetScore();
            showFloatingPoints();   
            totalScore += animal.GetScore();

            if (!animal.fotografiado)
            {
                animal.fotografiado = true;
               
                

                if (animal.animalAudioSource != null)
                {
                    animal.animalAudioSource.Play();
                }
            }

            if (animal.epica)
            {
                fotosEpicas++;
                ActivarMira(MiraExcelente);
                AnalyticsManager.Instance?.EnviarEventoTipoFoto("epica");
            }
            else
            {
                fotosBuenas++;
                ActivarMira(MiraBuena);
                AnalyticsManager.Instance?.EnviarEventoTipoFoto("buena");
            }
        }
        else
        {
            fotosDesen++;
            ActivarMira(MiraMala);
            AnalyticsManager.Instance?.EnviarEventoTipoFoto("mala");
        }

        cameraSound?.Play();
        yield return CaptureScreenshot();
        UpdateUI();

        if (photosRemaining <= 0)
        {
            StartCoroutine(RecargarFotos());  
        }
        else
        {
            yield return new WaitForSeconds(photoCooldown);
            ActivarMira(MiraDefault);
            canTakePhoto = true;
        }
    }
    void showFloatingPoints()
    {
        Animal animal = DetectarAnimal();
        if (animal != null)
        {
            pointsPerPhoto = animal.GetScore();
        }
        else
        {
            pointsPerPhoto = 0;
        }
        var go= Instantiate(floatingPointsPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshProUGUI>().text = "+" + (pointsPerPhoto).ToString();
    }
    IEnumerator RecargarFotos()
    {
        isReloading = true;
        canTakePhoto = false;
        Debug.Log("Recargando fotos...");
        photosRemainingText.text = "...";

        yield return new WaitForSecondsRealtime(rechargeTime);

        photosRemaining = maxPhotos;
        UpdateUI();
        Debug.Log("Recarga completa. Fotos restantes: " + photosRemaining);

        ActivarMira(MiraDefault);

        canTakePhoto = true;
        isReloading = false;
    }

    Animal DetectarAnimal()
    {
        Vector3 screenPos = apuntador.position;
        Ray ray = photoCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rangoRaycast, animalLayer))
        {
            return hit.collider.GetComponent<Animal>();
        }
        return null;
    }

    void UpdateUI()
    {
        photosRemainingText.text = $"{photosRemaining}";
        scoreText.text = $"{totalScore}";
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();
        uiCanvas.enabled = false;
        yield return new WaitForEndOfFrame();

        Texture2D fullScreenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        fullScreenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        fullScreenshot.Apply();
        uiCanvas.enabled = true;

        // Obtener las 4 esquinas del apuntador en mundo
        Vector3[] worldCorners = new Vector3[4];
        apuntador.GetWorldCorners(worldCorners);

        // Convertir esas esquinas a coordenadas de pantalla
        Vector2 screenBL = RectTransformUtility.WorldToScreenPoint(null, worldCorners[0]); // bottom-left
        Vector2 screenTR = RectTransformUtility.WorldToScreenPoint(null, worldCorners[2]); // top-right

        int width = Mathf.RoundToInt(screenTR.x - screenBL.x);
        int height = Mathf.RoundToInt(screenTR.y - screenBL.y);

        int x = Mathf.Clamp((int)screenBL.x, 0, Screen.width - width);
        int y = Mathf.Clamp((int)screenBL.y, 0, Screen.height - height);

        if (width <= 0 || height <= 0)
        {
            Destroy(fullScreenshot);
            yield break;
        }

        Texture2D croppedScreenshot = new Texture2D(width, height);
        croppedScreenshot.SetPixels(fullScreenshot.GetPixels(x, y, width, height));
        croppedScreenshot.Apply();

        Destroy(fullScreenshot);
        MostrarFotoEnUI(croppedScreenshot);
    }



    void MostrarFotoEnUI(Texture2D foto)
    {
        previewImage.sprite = Sprite.Create(foto, new Rect(0, 0, foto.width, foto.height), new Vector2(0.5f, 0.5f));
        StartCoroutine(FotoEnUI());
    }

    IEnumerator FotoEnUI()
    {
        previewCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(previewDuration);
        previewCanvasGroup.alpha = 0;
    }

    void ActivarMira(GameObject mira)
    {
        MiraDefault.SetActive(false);
        MiraMala.SetActive(false);
        MiraBuena.SetActive(false);
        MiraExcelente.SetActive(false);
        mira.SetActive(true);
    }
}
