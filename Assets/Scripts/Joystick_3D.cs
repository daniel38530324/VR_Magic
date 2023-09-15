using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick_3D : MonoBehaviour
{
    public Transform hand;
    public bool follow;

    [SerializeField] float forwardBackwardTilt = 0;
    [SerializeField] float sideToSideTilt = 0;
    [SerializeField] UnityEvent left, right, up, down, leftExit, rightExit, upExit, downExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(follow)
        {
            transform.LookAt(hand.position, transform.up);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JoystickTrigger_Left_2D"))
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

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            follow = true;
        }
        else if(other.CompareTag("NoHand"))
        {
            follow = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            follow = false;
        }
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
