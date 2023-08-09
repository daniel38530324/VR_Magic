using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingTarget : MonoBehaviour
{
    [SerializeField] TeachingManager teachingManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(AttackType.FireBall.ToString()) || other.CompareTag(AttackType.WaterBall.ToString()) || other.CompareTag(AttackType.LightningBall.ToString()))
        {
            teachingManager.UpdateTeachingState(TeachingState.Part3);
        }
    }
}
