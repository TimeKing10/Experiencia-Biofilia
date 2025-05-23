using UnityEngine;
using System.Collections.Generic;

public class FotoManager : MonoBehaviour
{
    public static FotoManager Instance;

    public Dictionary<string, Texture2D> fotosUltimasPorAnimal = new Dictionary<string, Texture2D>();

    private void Awake()
    {
        Instance = this;
    }

    public void GuardarFoto(string nombreAnimal, Texture2D foto, int score)
    {
        fotosUltimasPorAnimal[nombreAnimal] = foto;
    }
}
