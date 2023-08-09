using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Joystick_2D : MonoBehaviour
{
    [SerializeField] UnityEvent left, right, up, down, leftExit, rightExit, upExit, downExit;
    [SerializeField] Transform fingerPos, plane;
    Vector3 startPos;
    bool isTrigger;
 
    private void Start()
    {
        
    }

    private void Update()
    {
        if(isTrigger)
        {
            
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void onTouch(bool touch)
    {
        if (touch)
            isTrigger = true;
        else
            isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("JoystickBorder_2D"))
        {
            isTrigger = false;
        }
        if(other.CompareTag("JoystickTrigger_Left_2D"))
        {
            left.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Right_2D"))
        {
            right.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Up_2D"))
        {
            up.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Down_2D"))
        {
            down.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JoystickTrigger_Left_2D"))
        {
            leftExit.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Right_2D"))
        {
            rightExit.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Up_2D"))
        {
            upExit.Invoke();
        }
        if (other.CompareTag("JoystickTrigger_Down_2D"))
        {
            downExit.Invoke();
        }
    }
}
