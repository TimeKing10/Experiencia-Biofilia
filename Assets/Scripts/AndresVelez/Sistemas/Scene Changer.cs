using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Especifica el nombre de la escena en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnviarDatosYCambiarEscena();
        }
    }

    public void changeScene()
    {
        EnviarDatosYCambiarEscena();
    }

    private void EnviarDatosYCambiarEscena()
    {

        SceneManager.LoadScene(sceneName);
    }
}
