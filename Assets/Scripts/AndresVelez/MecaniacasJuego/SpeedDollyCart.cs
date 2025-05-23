using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpeedDollyCart : MonoBehaviour
{
    public Animator animator; // <-- Asigna esto desde el Inspector
    public CinemachineDollyCart dollyCart;
    public float newSpeed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("EmpezarAnimacion", true);
            dollyCart.m_Speed = newSpeed;
            
        }
    }
}

