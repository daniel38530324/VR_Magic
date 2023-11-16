using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //public Transform parent;
    public Transform objectA;
    public Transform objectB;

    public Transform cube;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(objectB.eulerAngles.x);

        //objectB.LookAt(cube.position, objectB.up);

        /*
        Vector3 targetEulerAngles = objectB.localEulerAngles;

        float targetX = targetEulerAngles.x;
        float targetY = targetEulerAngles.y;
        
        if (objectB.localEulerAngles.y > 40f && objectB.localEulerAngles.y < 180f)
        {
            targetY = 40;
        }
        else if (objectB.localEulerAngles.y < 320f && objectB.localEulerAngles.y > 180f)
        {
            targetY = 320;
        }
        
        if (objectB.localEulerAngles.x > 40f && objectB.localEulerAngles.x < 180f)
        {
            targetX = 40;
        }
        else if (objectB.localEulerAngles.x < 320 && objectB.localEulerAngles.x > 180f)
        {
            targetX = 320;
        }
        
        targetEulerAngles = new Vector3(targetX, targetY, targetEulerAngles.z);

        objectA.localRotation = Quaternion.Euler(targetEulerAngles);
        */

        Vector3 targetEulerAngles = objectB.eulerAngles;

        float targetX = targetEulerAngles.x;

        
        if (objectB.eulerAngles.x > 315 || objectB.eulerAngles.x < 270)
        {
            targetX = 315;
        }

        targetEulerAngles = new Vector3(targetX, targetEulerAngles.y, targetEulerAngles.z);

        objectA.rotation = Quaternion.Euler(targetEulerAngles);
    }
}
