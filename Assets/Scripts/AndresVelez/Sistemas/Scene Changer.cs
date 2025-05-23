using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Especifica el nombre de la escena en el Inspector
    public ResumenFinal resumenFinal;

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
        resumenFinal.MostrarResumen();
        /*ScoreManager.Instance.currentScore = TakePhotos.totalScore;
        ScoreManager.Instance.currentLevel = SceneManager.GetActiveScene().name;

        int updatedHighScore = await ScoreManager.Instance.SaveAndLoadUpdatedHighScore();

        ScoreManager.Instance.SubmitTotalScoreToLeaderboard();
*/
    }    
    public void loadEscena()
    {
        SceneManager.LoadScene(sceneName);
    }

}
