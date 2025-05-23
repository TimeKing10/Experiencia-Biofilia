using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("EmpezarAnimacion", true);
        }
    }
}
