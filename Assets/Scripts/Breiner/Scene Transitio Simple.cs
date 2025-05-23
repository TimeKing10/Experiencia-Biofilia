using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Collections;
using System.Threading.Tasks;

public class SceneTransitioSimple : MonoBehaviour
{
    public string nextSceneName = "NombreDeTuEscena";

    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            StartCoroutine(SignOutAndLoadScene());
        }
    }

    private IEnumerator SignOutAndLoadScene()
    {
        yield return EnsureUnityServicesInitialized();

        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Debug.Log("Sesión cerrada correctamente.");
        }
        else
        {
            Debug.LogWarning("No hay sesión iniciada para cerrar.");
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator EnsureUnityServicesInitialized()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            var initTask = UnityServices.InitializeAsync();

            while (!initTask.IsCompleted)
                yield return null;

            if (initTask.Exception != null)
            {
                Debug.LogError("Error al inicializar Unity Services: " + initTask.Exception);
            }
        }

        SceneManager.LoadScene(nextSceneName);
    }

}
