using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    float rotationSpeed = 30;
    private void Start()
    {
        
    }
    private void Update()
    {


        Vector3 lookDirection = objectB.forward;


        Quaternion targetRotation = objectB.rotation;


        if (objectB.rotation.eulerAngles.x > 315f || objectB.rotation.eulerAngles.x < 225f)
        {
            targetRotation = Quaternion.Euler(objectA.eulerAngles.x, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }


        objectA.rotation = targetRotation;
    }
}
