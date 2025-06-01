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
            Resumen();// ✅ Llamada directa
        }
    }

    private void Update()
    {
        Debug.Log(sceneName);
    }

    public void changeScene()
    {
        EnviarDatosYCambiarEscena(); // ✅ Llamada directa
    }

    private void Resumen() {
        resumenFinal.MostrarResumen();
    }

    public async void EnviarDatosYCambiarEscena()
    {
        
        ScoreManager.Instance.currentScore = TakePhotos.totalScore;
        ScoreManager.Instance.currentLevel = SceneManager.GetActiveScene().name;

        int updatedHighScore = await ScoreManager.Instance.SaveAndLoadUpdatedHighScore();

        ScoreManager.Instance.SubmitTotalScoreToLeaderboard();
        SceneManager.LoadScene(sceneName);

    }    
    public void loadEscena()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName);
    }

}
