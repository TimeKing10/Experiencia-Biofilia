using UnityEngine;
using DG.Tweening;
using TMPro;

public class NotificacionMision : MonoBehaviour
{
    public RectTransform panelTransform;      // Contenedor de la notificación
    public TextMeshProUGUI mensajeTexto;      // Texto de la notificación

    [Header("Posiciones de animación")]
    public Vector2 posicionEntrada = new Vector2(-600f, 0f);  // Fuera de pantalla (izquierda)
    public Vector2 posicionVisible = new Vector2(0f, 0f);     // En pantalla

    private void Awake()
    {
        panelTransform.anchoredPosition = posicionEntrada;
        gameObject.SetActive(false);
    }

    public void MostrarNotificacion(string mensaje)
    {
        mensajeTexto.text = mensaje;
        gameObject.SetActive(true);
        panelTransform.anchoredPosition = posicionEntrada;

        Sequence seq = DOTween.Sequence();
        seq.Append(panelTransform.DOAnchorPos(posicionVisible, 0.5f).SetEase(Ease.OutBack))
           .AppendInterval(2.5f)
           .Append(panelTransform.DOAnchorPos(posicionEntrada, 0.5f).SetEase(Ease.InBack))
           .OnComplete(() => gameObject.SetActive(false));
    }
}
