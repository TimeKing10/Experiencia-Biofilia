using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelInfoUI : MonoBehaviour
{
    public static LevelInfoUI Instance;

    public TMP_Text levelNameText;

    public TMP_Text levelTag;

    public TMP_Text[] levelObjectivesText;
    public TMP_Text levelDescriptionText;

    public Image[] speciesImageSlots;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateInfo(string name, string description, Sprite[] images, string tag, string[] objectives)
    {
        levelTag.text = tag;

    for (int i = 0; i < levelObjectivesText.Length; i++)
    {
        if (i < objectives.Length && !string.IsNullOrEmpty(objectives[i]))
        {
            levelObjectivesText[i].text = objectives[i];
            levelObjectivesText[i].gameObject.SetActive(true);
        }
        else
        {
            levelObjectivesText[i].gameObject.SetActive(false);
        }
    }

    levelNameText.text = name;
    levelDescriptionText.text = description;

        for (int i = 0; i < speciesImageSlots.Length; i++)
        {
            if (i < images.Length && images[i] != null)
            {
                speciesImageSlots[i].sprite = images[i];
                speciesImageSlots[i].gameObject.SetActive(true);
            }
            else
            {
                speciesImageSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
