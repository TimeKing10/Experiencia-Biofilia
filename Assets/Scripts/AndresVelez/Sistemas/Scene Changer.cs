using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Especifica el nombre de la escena en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnviarDatosYCambiarEscena(); // ✅ Llamada directa
        }
    }

    public void changeScene()
    {
        EnviarDatosYCambiarEscena(); // ✅ Llamada directa
    }

    private async void EnviarDatosYCambiarEscena()
    {
        ScoreManager.Instance.currentScore = TakePhotos.totalScore;
        ScoreManager.Instance.currentLevel = SceneManager.GetActiveScene().name;

        await ScoreManager.Instance.SaveAndLoadUpdatedHighScore();
        int updatedHighScore = await ScoreManager.Instance.SaveAndLoadUpdatedHighScore();

        ScoreManager.Instance.SubmitScoreToLeaderboard(
            ScoreManager.Instance.currentLevel,
            updatedHighScore
        );

        SceneManager.LoadScene(sceneName);
    }
}
