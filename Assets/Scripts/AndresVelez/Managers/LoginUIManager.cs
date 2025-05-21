using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoginUIManager : MonoBehaviour
{
    public static LoginUIManager Instance { get; private set; }

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI usernameDisplay;
    public TextMeshProUGUI errorText;

    async void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Inicializar Unity Services
        await UnityServices.InitializeAsync();
    }

    public async void OnLoginClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");

            // ✅ Solo actualizar si el nombre aún no ha sido establecido
            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
                Debug.Log($"Nombre de jugador actualizado a: {username}");
            }
            catch (RequestFailedException e)
            {
                Debug.Log("El nombre ya está establecido o no se puede cambiar más: " + e.Message);
            }

            ShowUsername();
            errorText.text = "";

            SceneManager.LoadScene("Mapa");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            errorText.text = "Error al iniciar sesión: " + ex.Message;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            errorText.text = "Error de red: " + ex.Message;
        }
    }


    public async void OnSignUpClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!IsValidUsername(username) || !IsValidPassword(password))
        {
            errorText.text = "El usuario debe tener 3-20 caracteres (letras, números, ., -, @, _).\n" +
                             "La contraseña debe tener 8-30 caracteres, con mayúsculas, minúsculas, un número y un símbolo.";
            return;
        }

        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");

            // ✅ Establecer nombre visible del jugador
            await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
            Debug.Log($"Nombre de jugador actualizado a: {username}");

            ShowUsername();
            errorText.text = "";

            SceneManager.LoadScene("Mapa");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            errorText.text = "Error al registrarse: " + ex.Message;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            errorText.text = "Error de red: " + ex.Message;
        }
    }


    void ShowUsername()
    {
        usernameDisplay.text = $"Usuario: {usernameInput.text}";
    }

    bool IsValidUsername(string username)
    {
        if (string.IsNullOrEmpty(username)) return false;
        if (username.Length < 3 || username.Length > 20) return false;
        return System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9.\-@_]+$");
    }

    bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        if (password.Length < 8 || password.Length > 30) return false;

        bool hasUpper = false, hasLower = false, hasDigit = false, hasSymbol = false;
        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpper = true;
            else if (char.IsLower(c)) hasLower = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else hasSymbol = true;
        }
        return hasUpper && hasLower && hasDigit && hasSymbol;
    }
}
