using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoint : MonoBehaviour
{
    public int num;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Level1Manager.currentState = num;
            Destroy(gameObject);
        }
    }
}
