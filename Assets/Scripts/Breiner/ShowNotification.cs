using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShowNotification : MonoBehaviour
{
    [SerializeField] private string notificationMessage;
    [SerializeField] private float delayTimetoHide = 3f;
    [SerializeField] private float yOffset = 100f; // Desplazamiento vertical inicial y final

    public GameObject notificationPanel;

    private TextMeshProUGUI textComponent;
    private Vector3 originalPosition;

    void Start()
    {
        if (notificationPanel != null)
        {
            originalPosition = notificationPanel.transform.localPosition;
            notificationPanel.SetActive(false);

            textComponent = notificationPanel.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Show(notificationMessage);
        }
    }

    public void Show(string message)
    {
        if (notificationPanel == null) return;

        notificationPanel.SetActive(true);

        // Posición inicial desplazada hacia arriba
        notificationPanel.transform.localPosition = originalPosition + new Vector3(0, yOffset, 0);

        if (textComponent != null)
        {
            textComponent.text = message;
        }

        // Animación: baja hacia su posición original (como un cartel cayendo)
        notificationPanel.transform
            .DOLocalMove(originalPosition, 0.4f)
            .SetEase(Ease.InBack);

        Invoke(nameof(Hide), delayTimetoHide);
    }

    public void Hide()
    {
        if (notificationPanel == null) return;

        // Animación: sube de nuevo (como si se recogiera)
        notificationPanel.transform
            .DOLocalMove(originalPosition + new Vector3(0, yOffset, 0), 0.3f)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                notificationPanel.SetActive(false);
            });
    }
}
