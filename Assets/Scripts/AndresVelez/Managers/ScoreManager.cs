using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int currentScore = 0;
    public string currentLevel;

    async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        await InitializeUnityServicesAsync();
    }

    private async Task InitializeUnityServicesAsync()
    {
        try
        {
            if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Services initialized from ScoreManager.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error initializing Unity Services: " + ex.Message);
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public async Task<int> SaveAndLoadUpdatedHighScore()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("No se puede guardar: el usuario no está autenticado.");
            return 0;
        }

        int previousHighScore = await LoadHighScore(currentLevel);
        if (currentScore > previousHighScore)
        {
            string key = GetHighScoreKey(currentLevel);

            var data = new Dictionary<string, object>
        {
            { key, currentScore }
        };

            try
            {
                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
                Debug.Log($"Nuevo high score guardado para {key}: {currentScore}");

                await Task.Delay(300); // Esperar un poco para asegurar que se refleje en la nube
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error al guardar en la nube: " + ex.Message);
            }
        }

        int updated = await LoadHighScore(currentLevel);
        Debug.Log($"High score actualizado y cargado: {updated}");
        return updated;
    }


    public async Task<int> LoadHighScore(string levelName)
    {
        string key = GetHighScoreKey(levelName);

        try
        {
            var savedData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });

            if (savedData.TryGetValue(key, out var item))
            {
                return item.Value.GetAs<int>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al cargar desde la nube: " + ex.Message);
        }

        return 0;
    }

    private string GetHighScoreKey(string levelName)
    {
        return "HighScore_" + levelName.Replace(" ", "_");
    }
}
