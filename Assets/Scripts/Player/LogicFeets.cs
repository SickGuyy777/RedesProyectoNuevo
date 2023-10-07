using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicFeets : MonoBehaviour
{
    public PlayerMovement pl;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            pl.IcanJump = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            pl.IcanJump = false;
        }
    }
}
