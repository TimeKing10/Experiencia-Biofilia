using UnityEngine;

public class LoginUIConnector : MonoBehaviour
{
    private LoginUIManager loginManager;

    void Awake()
    {
        // Buscar dinámicamente al LoginUIManager, incluso si viene de otra escena
        loginManager = FindObjectOfType<LoginUIManager>();

        if (loginManager == null)
        {
            Debug.LogWarning("No se encontró LoginUIManager en la escena.");
        }
    }

    public void OnLoginButtonPressed()
    {
        if (loginManager != null)
            loginManager.OnLoginClicked();
        else
            Debug.LogError("LoginUIManager no encontrado al hacer login.");
    }

    public void OnSignUpButtonPressed()
    {
        if (loginManager != null)
            loginManager.OnSignUpClicked();
        else
            Debug.LogError("LoginUIManager no encontrado al hacer signup.");
    }
}
