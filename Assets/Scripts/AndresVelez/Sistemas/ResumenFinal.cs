using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor;
public class ResumenFinal : MonoBehaviour
{
    public Transform panelFotosAnimales;
    public GameObject prefabAnimalItem;

    void Start()
    {
        MostrarFotosMejores();
    }

    void MostrarFotosMejores()
    {
        foreach (var entry in TakePhotos.mejoresFotos.Values)
        {
            GameObject item = Instantiate(prefabAnimalItem, panelFotosAnimales);
            var texts = item.GetComponentsInChildren<TextMeshProUGUI>();
            var images = item.GetComponentsInChildren<Image>();

            // Asignar texto y sprite
            foreach (var text in texts)
                text.text = entry.nombreAnimal;

            foreach (var img in images)
                if (entry.foto != null)
                    img.sprite = Sprite.Create(entry.foto, new Rect(0, 0, entry.foto.width, entry.foto.height), new Vector2(0.5f, 0.5f));
        }
    }
}
