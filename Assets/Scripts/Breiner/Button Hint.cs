using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHint : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hintImage;

    void Start()
    {
        if (hintImage != null)
            hintImage.SetActive(false);
    }

    public void ShowHint()
    {
        if (hintImage != null)
            hintImage.SetActive(true);
    }

    public void HideHint()
    {
        if (hintImage != null)
            hintImage.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ShowHint();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HideHint();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHint();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHint();
    }
}
