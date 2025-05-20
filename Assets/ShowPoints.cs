using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ShowPoints : MonoBehaviour
{
    public float lifeTime = 1f;
    public float popScale = 1.2f;
    public float moveUpDistance = 1f;
    public float exitDuration = 0.3f;

    public Vector3 offset; // Offset for the floating points

    void Start()
    {
        transform.localPosition += offset; // Set the initial position with offset
        Vector3 originalScale = transform.localScale;

        transform.DOScale(originalScale * popScale, 0.2f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutSine);
            });

        transform.DOMoveY(transform.position.y + moveUpDistance, lifeTime).SetEase(Ease.OutSine);

        StartCoroutine(AnimateAndDestroy());
    }

    IEnumerator AnimateAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime - exitDuration);
        transform.DOScale(Vector3.zero, exitDuration).SetEase(Ease.InBack);
        yield return new WaitForSeconds(exitDuration);
        Destroy(gameObject);
    }
}
