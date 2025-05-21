using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using TMPro;
using System.Threading.Tasks;

public class LeaderboardUI : MonoBehaviour
{
    public string leaderboardId = "leaderboard_amazonas"; // ID exacto del leaderboard
    public Transform contentParent;                       // Contenedor en el Canvas donde van las filas
    public GameObject entryPrefab;                         // Prefab con LeaderboardEntryUI

    async void Start()
    {
        await InitializeAndLoadLeaderboard();
    }

    private async Task InitializeAndLoadLeaderboard()
    {
        try
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            await LoadLeaderboard();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error inicializando servicios o cargando leaderboard: " + ex.Message);
        }
    }

    public async Task LoadLeaderboard()
    {
        try
        {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions
            {
                Limit = 20
            });

            foreach (Transform child in contentParent)
                Destroy(child.gameObject);

            foreach (var entry in scoresResponse.Results)
            {
                GameObject obj = Instantiate(entryPrefab, contentParent);
                var entryUI = obj.GetComponent<LeaderboardEntryUI>();

                entryUI.SetData(
                    entry.Rank + 1,
                    entry.PlayerName ?? "Anónimo",
                    (int)entry.Score  // <-- casteo explícito a int
                );
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al cargar leaderboard: " + ex.Message);
        }
    }
}
