using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] Transform target;
    bool follow = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Finger"))
        {
            if (!follow) { return; }
            target.GetComponent<Joystick_2D_2>().OnTouch(true);
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            target.position = closestPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finger"))
        {
            target.GetComponent<Joystick_2D_2>().OnTouch(false);
            //target.localPosition = Vector3.zero;
        }
    }

    public void SetFollow(bool state)
    {
        follow = state;
    }
}
