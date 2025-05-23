using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ResumenFinal : MonoBehaviour
{
    [System.Serializable]
    public class FotoFinalSlot
    {
        public string nombreAnimal;
        public Image imagenUI;
    }

    public List<FotoFinalSlot> slotsFinales = new List<FotoFinalSlot>();
    public GameObject panelFotos;

    [Header("Resumen numérico")]
    public GameObject textoEstrellas;

    public TMP_Text textoaCopiar;
    public TMP_Text textoPuntaje;


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

    public void MostrarResumenFinal(string puntajeTotal)
    {
        if (textoEstrellas != null)
        {
            textoEstrellas.SetActive(true);
            
        }

        if (textoPuntaje != null)
            textoPuntaje.text = textoaCopiar.text; 
    }

    public void MostrarResumen()
    {

        if (panelFotos != null)
        {
            MostrarResumenFinal(textoPuntaje.text);
            panelFotos.SetActive(true);

        }
        
    }
}
