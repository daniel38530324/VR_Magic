using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick_2D_2 : MonoBehaviour
{
    public Transform hand;

    [SerializeField] UnityEvent left, right, up, down, leftExit, rightExit, upExit, downExit;

    bool follow;
    //Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (follow)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = hand.transform.position.x;
            newPosition.y = hand.transform.position.y;
            //newPosition.z = -15.1192f;
            transform.position = hand.transform.position;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        */
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
