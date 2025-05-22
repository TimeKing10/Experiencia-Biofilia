using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButtonUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string levelDescription;
    public string levelName;
    public Sprite[] levelImages;
    public string levelTag;
    public string[] levelObjectivesText;

    public GameObject startTag;

    private bool isPointerOver = false;
    private bool isSelected = false;

    void Update()
    {
        // Si no está seleccionado ni el mouse está encima, ocultar startTag
        if (!isPointerOver && !isSelected)
        {
            if (startTag.activeSelf)
                startTag.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        UpdateUI();
        startTag.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        // El Update() se encargará de ocultar startTag si también el mouse está fuera
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        UpdateUI();
        startTag.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        // El Update() se encargará de ocultar startTag si también no está seleccionado
    }

    void UpdateUI()
    {
        LevelInfoUI.Instance.UpdateInfo(levelName, levelDescription, levelImages, levelTag, levelObjectivesText);
    }
}
