using UnityEngine;

public class Animal : MonoBehaviour
{
    public int baseScore = 100;         
    public float epicMultiplier = 2f;   
    public string nombreAnimal;
    public AudioSource animalAudioSource;
    public bool fotografiado = false;
    public bool epica = false;

    public Animator animator;

    

    void Update()
    {
        VerificarAnimacionEpica();
    }

    void VerificarAnimacionEpica()
    {
        if (animator == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Epica")) 
        {
            epica = true;
        }
        else
        {
            epica = false;
        }
    }

    public int GetScore()
    {
        return epica ? Mathf.RoundToInt(baseScore * epicMultiplier) : baseScore;
    }
}



