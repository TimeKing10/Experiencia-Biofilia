using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButtonUI : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [TextArea]
    public string levelDescription;
    public string levelName;
    public Sprite[] levelImages;

    public string levelTag;

    public string[] levelObjectivesText;

    public void OnSelect(BaseEventData eventData)
    {
        UpdateUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        LevelInfoUI.Instance.UpdateInfo(levelName, levelDescription, levelImages, levelTag, levelObjectivesText);
    }
}
