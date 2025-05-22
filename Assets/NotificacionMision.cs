using UnityEngine;
using DG.Tweening;
using TMPro;

public class NotificacionMision : MonoBehaviour
{
    public RectTransform panelTransform; // El contenedor del panel de notificación
    public TextMeshProUGUI mensajeTexto; // El texto a mostrar

    private Vector2 posicionFuera; // posición inicial (fuera de pantalla)
    private Vector2 posicionVisible; // posición destino visible

    void Awake()
    {
        // Guarda las posiciones de entrada y salida (fuera de pantalla = -600 px en X)
        posicionVisible = panelTransform.anchoredPosition;
        posicionFuera = new Vector2(-600f, posicionVisible.y);

        panelTransform.anchoredPosition = posicionFuera;
        gameObject.SetActive(true);
    }

    public void MostrarNotificacion(string mensaje)
    {
        mensajeTexto.text = mensaje;
        gameObject.SetActive(true);

        panelTransform.anchoredPosition = posicionFuera;

        Sequence seq = DOTween.Sequence();
        seq.Append(panelTransform.DOAnchorPosX(posicionVisible.x, 0.5f).SetEase(Ease.OutBack))
           .AppendInterval(2.5f)
           .Append(panelTransform.DOAnchorPosX(posicionFuera.x, 0.5f).SetEase(Ease.InBack))
           .OnComplete(() => gameObject.SetActive(false));
    }
}
