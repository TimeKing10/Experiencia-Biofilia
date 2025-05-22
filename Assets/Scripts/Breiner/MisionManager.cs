using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class MisionManager : MonoBehaviour
{
    public List<MisionConfig> misiones = new List<MisionConfig>();
    public Image[] misionLeave;

    [SerializeField] private Sprite Acomplished;
    [SerializeField] private Sprite Non;
    public Slider progressBar;

    public Animator starAnimator;

    // Notificación UI
    [Header("Notificación de Misión")]
    public NotificacionMision notificacion; // Referencia al script de notificación

    void Start()
    {
        foreach (Image img in misionLeave)
        {
            if (img != null) img.sprite = Non;
        }

        progressBar.value = 0f;
        progressBar.maxValue = misiones.Count;
    }

    void Update()
    {
        int completadas = 0;

        for (int i = 0; i < misiones.Count; i++)
        {
            var m = misiones[i];

            if (!m.completada && EvaluarCondicion(m))
            {
                m.completada = true;
                Debug.Log($"¡Misión completada!: {m.descripcion}");

                if (AnalyticsManager.Instance != null)
                {
                    AnalyticsManager.Instance.EnviarEventoMision(i + 1, m.descripcion);
                }

                // Mostrar notificación con la descripción de la misión
                if (notificacion != null)
                {
                    notificacion.MostrarNotificacion($"Lograste: {m.descripcion}");
                }

                if (starAnimator != null)
                {
                    starAnimator.SetTrigger("Shine");
                }
            }

            if (m.completada) completadas++;
        }

        for (int i = 0; i < completadas && i < misionLeave.Length; i++)
        {
            misionLeave[i].sprite = Acomplished;
        }

        progressBar.value = completadas;
    }

    bool EvaluarCondicion(MisionConfig m)
    {
        int valorActual = 0;

        switch (m.variable)
        {
            case VariableMision.FotosBuenas:
                valorActual = TakePhotos.fotosBuenas;
                break;
            case VariableMision.FotosEpicas:
                valorActual = TakePhotos.fotosEpicas;
                break;
            case VariableMision.PuntajeTotal:
                valorActual = TakePhotos.totalScore;
                break;
        }

        return m.mayorQue ? valorActual > m.valorObjetivo : valorActual <= m.valorObjetivo;
    }
}

public enum VariableMision
{
    FotosBuenas,
    FotosEpicas,
    PuntajeTotal
}

[Serializable]
public class MisionConfig
{
    public string descripcion;
    public VariableMision variable;
    public bool mayorQue;
    public int valorObjetivo;
    [HideInInspector] public bool completada = false;
}
