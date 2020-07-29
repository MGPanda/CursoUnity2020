using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    public Transform observed;

    private bool isPlayerInRange, playerCaught;

    public GameEnding gameEnding;

    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = observed.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            
            Debug.DrawRay(transform.position, direction, Color.green, Time.deltaTime, 
                true);
            
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == observed)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == observed)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == observed)
        {
            isPlayerInRange = false;
        }
    }
}
