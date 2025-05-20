using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    void Start()
    {
        string nivel = SceneManager.GetActiveScene().name;

        ScoreManager.Instance.currentLevel = nivel;
        ScoreManager.Instance.ResetCurrentScore();
    }

    public void GanarPuntos(int puntos)
    {
        ScoreManager.Instance.AddScore(puntos);
    }

    public async void TerminarNivel()
    {
        int nuevoRecord = await ScoreManager.Instance.SaveAndLoadUpdatedHighScore();

        Debug.Log($"[NivelManager] High score final del nivel: {nuevoRecord}");

        SceneManager.LoadScene("Mapa");
    }
}

