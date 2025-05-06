using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransitioSimple : MonoBehaviour
{
    public string nextSceneName = "NombreDeTuEscena";

    

    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}