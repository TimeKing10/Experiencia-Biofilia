using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;

public class CloudInitializer : MonoBehaviour
{
    async void Awake()
    {
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized. Waiting for login...");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error initializing Unity Services: " + ex.Message);
        }
    }
}

