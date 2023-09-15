using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowJoystick : MonoBehaviour
{
    [SerializeField] Transform objectA;
    [SerializeField] Transform objectB;
    [SerializeField] Joystick_3D joystick_3D;

    void Update()
    {
        if (joystick_3D.follow)
        {
            objectA.transform.position = objectB.transform.position;
        }
        else
        {
            objectA.localPosition = Vector3.zero;
        }
    }
}
