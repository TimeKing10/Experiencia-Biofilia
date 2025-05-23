using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResumenFinal : MonoBehaviour
{
    [System.Serializable]
    public class FotoFinalSlot
    {
        public string nombreAnimal; // Identificador único para cada animal
        public Image imagenUI;
    }

    public List<FotoFinalSlot> slotsFinales = new List<FotoFinalSlot>();
    public GameObject panelFotos; // Panel que contiene las imágenes y está apagado

    public void ActualizarImagenUI(string nombreAnimal, Texture2D foto)
    {
        foreach (var slot in slotsFinales)
        {
            if (slot.nombreAnimal == nombreAnimal)
            {
                slot.imagenUI.sprite = Sprite.Create(foto, new Rect(0, 0, foto.width, foto.height), new Vector2(0.5f, 0.5f));
                slot.imagenUI.gameObject.SetActive(true);
                break;
            }
        }
    }

    public List<string> ObtenerNombresAnimales()
    {
        List<string> nombres = new List<string>();
        foreach (var slot in slotsFinales)
        {
            nombres.Add(slot.nombreAnimal);
        }
        return nombres;
    }

    public void MostrarResumen()
    {
        if (panelFotos != null)
        {
            panelFotos.SetActive(true);
        }
    }
}
