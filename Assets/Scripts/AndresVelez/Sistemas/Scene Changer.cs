using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks; // ← Necesario para Task

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Especifica el nombre de la escena en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ = EnviarDatosYCambiarEscenaAsync(); // Usamos async correctamente
        }
    }

    public void changeScene()
    {
        _ = EnviarDatosYCambiarEscenaAsync(); // También aquí
    }

    // 🔹 Método async para guardar datos y luego cambiar de escena
    private async Task EnviarDatosYCambiarEscenaAsync()
    {
        ScoreManager.Instance.currentScore = TakePhotos.totalScore;
        ScoreManager.Instance.currentLevel = SceneManager.GetActiveScene().name;

        await ScoreManager.Instance.SaveAndLoadUpdatedHighScore(); // ← Usamos await aquí

        SceneManager.LoadScene(sceneName); // ← Solo se ejecuta después de guardar
    }
}
