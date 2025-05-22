using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public RectTransform apuntador;
    public RectTransform canvasRectTransform;
    public float pointerSpeed = 300f;

    private Vector2 moveInput;
    private Vector2 canvasMin;
    private Vector2 canvasMax;

    void Start()
    {
        canvasRectTransform = Referencias.Instance.canva;

        // Establecer el canvas como padre y reiniciar transformaciones
        transform.SetParent(canvasRectTransform, false); // <- importante: 'false' mantiene la escala/posición local

        // Centrar este objeto (jugador) si es un RectTransform
        if (transform is RectTransform jugadorRect)
        {
            jugadorRect.anchoredPosition = Vector2.zero;
            jugadorRect.localRotation = Quaternion.identity;
            jugadorRect.localScale = Vector3.one;
        }

        // Centrar también el apuntador
        if (apuntador != null)
        {
            apuntador.anchoredPosition = Vector2.zero;
        }

        // Calcular los límites del canvas
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        canvasMin = -canvasSize / 2f;
        canvasMax = canvasSize / 2f;
    }


    private void Update()
    {
        MoverCursor();
    }

    void MoverCursor()
    {
        apuntador.anchoredPosition += moveInput * pointerSpeed * Time.deltaTime;
        apuntador.anchoredPosition = new Vector2(
            Mathf.Clamp(apuntador.anchoredPosition.x, canvasMin.x, canvasMax.x),
            Mathf.Clamp(apuntador.anchoredPosition.y, canvasMin.y, canvasMax.y)
        );
    }

    // 🚀 Este método es llamado automáticamente por PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
