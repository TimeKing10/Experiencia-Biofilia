using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class ResumenFinal : MonoBehaviour
{
    [System.Serializable]
    public class FotoFinalSlot
    {
        public string nombreAnimal;
        public Image imagenUI;
    }

    [Header("Fotos de animales")]
    public List<FotoFinalSlot> slotsFinales = new List<FotoFinalSlot>();
    public GameObject panelFotos;

    public GameObject hudGameplay; // Referencia al HUD del juego

    [Header("Resumen numérico")]
    public Image[] estrellas; // Arreglo de estrellas a animar
    public TMP_Text textoPuntaje;
    public TMP_Text textoaCopiar; // Fuente del puntaje real a mostrar

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip popSound;

    [Header("Animación")]
    public float delayEntreEstrellas = 0.4f;
    public float delayEntreFotos = 0.2f;
    public float duracionContador = 1.2f;

    public void ActualizarImagenUI(string nombreAnimal, Texture2D foto)
    {
        foreach (var slot in slotsFinales)
        {
            if (slot.nombreAnimal == nombreAnimal)
            {
                slot.imagenUI.sprite = Sprite.Create(foto, new Rect(0, 0, foto.width, foto.height), new Vector2(0.5f, 0.5f));
                slot.imagenUI.gameObject.SetActive(false); // Se activa más tarde con animación
                break;
            }
        }
    }

    public List<string> ObtenerNombresAnimales()
    {
        List<string> nombres = new List<string>();
        foreach (var slot in slotsFinales)
        {
            nombres.Add(slot.nombreAnimal);
        }
        return nombres;
    }

    public void MostrarResumen()
    {
        if (panelFotos != null)
        {
            // Desactivar HUD del juego
            if (hudGameplay != null)
            {
                hudGameplay.SetActive(false);
            }
            panelFotos.SetActive(true);
            panelFotos.transform.localScale = Vector3.zero;

            // Escalado inicial tipo pop del panel
            panelFotos.transform.DOScale(1f, 0.35f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => StartCoroutine(AnimarResumen()));
        }
    }

    private IEnumerator AnimarResumen()
    {
        // Animar estrellas una por una
        foreach (var estrella in estrellas)
{
    if (estrella != null)
    {
        estrella.gameObject.SetActive(true); // Activar primero
        estrella.transform.localScale = Vector3.zero; // Reiniciar escala
        estrella.transform.DOScale(1f, 0.6f)           // Animar pop
            .SetEase(Ease.OutBack);

        audioSource?.PlayOneShot(popSound);
        yield return new WaitForSeconds(delayEntreEstrellas);
    }
}

        // Fotos (animación tipo pop para cada imagen activa)
        foreach (var slot in slotsFinales)
        {
            if (slot.imagenUI != null && slot.imagenUI.sprite != null)
            {
                slot.imagenUI.transform.localScale = Vector3.zero;
                slot.imagenUI.gameObject.SetActive(true);
                slot.imagenUI.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

                audioSource?.PlayOneShot(popSound);
                yield return new WaitForSeconds(delayEntreFotos);
            }
        }

        // Puntaje animado
        if (textoPuntaje != null && textoaCopiar != null)
        {
            int puntajeFinal = 0;
            int.TryParse(textoaCopiar.text, out puntajeFinal);

            int valorActual = 0;
            DOTween.To(() => valorActual, x => {
                valorActual = x;
                textoPuntaje.text = valorActual.ToString();
            }, puntajeFinal, duracionContador).SetEase(Ease.OutCubic);
        }
    }
}
