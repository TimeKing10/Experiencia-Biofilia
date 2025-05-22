using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransitioSimple : MonoBehaviour
{
    public string nextSceneName = "NombreDeTuEscena";

    

    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SignOut();
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void SignOut()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Debug.Log("Sesi�n cerrada correctamente.");
        }
        else
        {
            Debug.LogWarning("No hay sesi�n iniciada para cerrar.");
        }
    }
}