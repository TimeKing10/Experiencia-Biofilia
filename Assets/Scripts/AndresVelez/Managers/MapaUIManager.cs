using UnityEngine;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class MapaUIManager : MonoBehaviour
{
    public TextMeshProUGUI nivel1RecordText;
    public TextMeshProUGUI nivel2RecordText;
    public TextMeshProUGUI nivel3RecordText;

    async void Start()
    {
        // Asegura que los servicios estén inicializados
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await UnityServices.InitializeAsync();
        }

        // Asegura que el usuario esté autenticado
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogError("El usuario no está autenticado. No se puede cargar el score.");
            return;
        }

        // Cargar puntajes si todo está bien
        int score1 = await ScoreManager.Instance.LoadHighScore("Nivel amazonas");
        nivel1RecordText.text = "Record: " + score1;

        int score2 = await ScoreManager.Instance.LoadHighScore("Nivel Tatacoa");
        nivel2RecordText.text = "Record: " + score2;
    }
}
