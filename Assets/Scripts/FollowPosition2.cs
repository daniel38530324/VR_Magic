using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition2 : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    float rotationSpeed = 30;
    private void Start()
    {

    }
    private void Update()
    {

        Vector3 targetPosition = objectB.position;
        Vector3 targetLocalPosition = objectB.localPosition;


        if (objectB.localPosition.x > 0.05f)
        {
            targetLocalPosition.x = 0.05f;
            
        }
        else if(objectB.localPosition.x < -0.05f)
        {
            targetLocalPosition.x = -0.05f;
        }

        if (objectB.localPosition.z > 0.05f)
        {
            targetLocalPosition.z = 0.05f;

        }
        else if (objectB.localPosition.z < -0.05f)
        {
            targetLocalPosition.z = -0.05f;
        }
        /*
        if (objectB.localPosition.z > 0.2f || objectB.localPosition.z < -0.2f)
        {
            targetPosition.z = objectA.position.z;
        }

        objectA.position = targetPosition;
        */
        objectA.localPosition = targetLocalPosition;
    }
}
