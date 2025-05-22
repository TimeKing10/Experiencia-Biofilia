using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    public TextMeshProUGUI NameText;   // Referencia al texto para el nombre del jugador
    public TextMeshProUGUI ScoreText;  // Referencia al texto para el puntaje

    public void SetData(int rank, string playerName, int score)
    {
        NameText.text = $"{rank}. {playerName}";
        ScoreText.text = score.ToString();
    }
}
