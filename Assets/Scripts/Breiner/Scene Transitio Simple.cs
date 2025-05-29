using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Collections;

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
        // Asegúrate de que Unity Services esté inicializado
        yield return EnsureUnityServicesInitialized();

        // Cerrar sesión si está iniciada
        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Debug.Log("Sesión cerrada correctamente.");

            // Opcional: esperar un frame para asegurar que el cierre tenga efecto
            yield return null;
        }
        else
        {
            Debug.LogWarning("No hay sesión iniciada para cerrar.");
        }

        // Cargar la nueva escena
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

        // No cargar la escena aquí, solo asegurarse de que los servicios están listos
        yield return null;
    }
}
