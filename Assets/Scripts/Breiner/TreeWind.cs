using UnityEngine;

public class TreeWind : MonoBehaviour
{
    public float minSwayAmount = 0.5f; // M�nimo �ngulo de inclinaci�n
    public float maxSwayAmount = 3f;   // M�ximo �ngulo de inclinaci�n
    public float swaySpeed = 0.5f;     // Velocidad del viento

    private float swayAmount;  // �ngulo m�ximo de inclinaci�n (aleatorio)
    private float randomOffset; // Variaci�n �nica para cada �rbol

    void Start()
    {
        swayAmount = Random.Range(minSwayAmount, maxSwayAmount); // Rango editable
        randomOffset = Random.Range(0f, 100f); // Variaci�n por �rbol
    }

    void Update()
    {
        float sway = (Mathf.PerlinNoise(Time.time * swaySpeed, randomOffset) - 0.5f) * 2f * swayAmount;
        transform.rotation = Quaternion.Euler(sway, 0, 0);
    }
}
