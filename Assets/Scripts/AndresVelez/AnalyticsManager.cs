using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    private bool _isInitialized = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private async void Start()
    {
        Debug.Log("Awake ha sido llamado");

        await UnityServices.InitializeAsync();

        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            AnalyticsService.Instance.StartDataCollection();
            _isInitialized = true;
            Debug.Log("Unity Services inicializados correctamente");
        }
        else
        {
            Debug.LogError("Unity Services no se inicializaron correctamente");
        }
    }

    // 📷 Evento de tipo de foto tomada
    public void EnviarEventoTipoFoto(string tipo)
    {
        if (!_isInitialized) return;

        CustomEvent eventoFoto = new CustomEvent("FotoTomada")
        {
            { "tipoFoto", tipo }
        };

        AnalyticsService.Instance.RecordEvent(eventoFoto);
        Debug.Log("📸 Evento de foto enviado: " + tipo);
    }

    // ✅ Evento de misión individual completada
    public void EnviarEventoMision(int numeroMision, string descripcion)
    {
        if (!_isInitialized) return;

        CustomEvent evento = new CustomEvent("mision_completada")
        {
            { "mision_id", numeroMision },
            { "descripcion", descripcion }
        };

        AnalyticsService.Instance.RecordEvent(evento);
        Debug.Log($"📨 Evento de misión enviada: Misión {numeroMision} - {descripcion}");
    }
}
