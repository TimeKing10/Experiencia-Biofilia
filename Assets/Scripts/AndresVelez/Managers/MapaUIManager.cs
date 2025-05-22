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
        nivel1RecordText.text = "R�cord: " + score1;

        // Puedes agregar m�s niveles as�:
        // int score2 = await ScoreManager.Instance.LoadHighScore("Nivel sabana");
        // nivel2RecordText.text = "R�cord: " + score2;
    }
}
