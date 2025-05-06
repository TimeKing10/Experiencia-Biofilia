using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoAnim : MonoBehaviour
{
    public RectTransform logoTransform;   // Logo principal
    public RectTransform highlight;       // Efecto de brillo (Image)

    [Header("Animaci�n de entrada")]
    public float startScale = 0.5f;
    public float growDuration = 1.2f;

    [Header("Efecto de Brillo Err�tico")]
    public float moveDurationMin = 0.3f;
    public float moveDurationMax = 1.5f;
    public float highlightRangeX = 50f; // M�ximo desplazamiento en X
    public float highlightRangeY = 20f; // M�ximo desplazamiento en Y

    [Header("Movimiento del Logo")]
    public float bounceAmount = 5f;
    public float bounceDuration = 1.5f;

    private void Start()
    {
        AnimateLogo();
    }

    void AnimateLogo()
    {
        // Iniciar el logo peque�o y hacerlo crecer
        logoTransform.localScale = Vector3.one * startScale;
        logoTransform.DOScale(Vector3.one, growDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                if (highlight.gameObject.activeInHierarchy) // Evita conflicto si ya est� en uso
                {
                    StartErraticHighlight();
                }
                StartBounce();
            });
    }

    void StartErraticHighlight()
    {
        if (!highlight.gameObject.activeInHierarchy) return; // No ejecutar si est� desactivado

        float randomX = Random.Range(-highlightRangeX, highlightRangeX);
        float randomY = Random.Range(-highlightRangeY, highlightRangeY);
        float randomDuration = Random.Range(moveDurationMin, moveDurationMax);

        highlight.DOAnchorPos(new Vector2(randomX, randomY), randomDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => StartErraticHighlight()); // Se repite indefinidamente
    }

    void StartBounce()
    {
        logoTransform.DOAnchorPosY(logoTransform.anchoredPosition.y + bounceAmount, bounceDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
