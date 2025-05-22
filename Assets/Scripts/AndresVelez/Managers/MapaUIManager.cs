using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class MapaUIManager : MonoBehaviour
{
    public TextMeshProUGUI nivel1RecordText;
    public TextMeshProUGUI nivel2RecordText;
    public TextMeshProUGUI nivel3RecordText;

    async void Start()
    {
        int score1 = await ScoreManager.Instance.LoadHighScore("Nivel amazonas");
        nivel1RecordText.text = "Récord: " + score1;

        // Puedes agregar más niveles así:
        // int score2 = await ScoreManager.Instance.LoadHighScore("Nivel sabana");
        // nivel2RecordText.text = "Récord: " + score2;
    }
}
