using UnityEngine;
using UnityEngine.InputSystem;

public class PausaController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public InputActionAsset inputActions;

    private InputAction pauseAction;
    private bool isPaused = false;

    private void OnEnable()
    {
        // Activar el mapa "Pausa"
        var mapaPausa = inputActions.FindActionMap("Pausa", true);
        mapaPausa.Enable();

        // Obtener y suscribirse a la acción "Pause"
        pauseAction = mapaPausa.FindAction("Pause", true);
        pauseAction.performed += OnPausePressed;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        pauseAction.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext ctx)
    {
        if (isPaused)
            Reanudar();
        else
            Pausar();
    }

    void Pausar()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
