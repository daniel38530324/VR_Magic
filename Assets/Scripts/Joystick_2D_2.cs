using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick_2D_2 : MonoBehaviour
{
    [SerializeField] Transform startPos, camera;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 2.5f;

    private Vector3 startPosition, inputDirection, moveDirection;
    bool isTrigger;

    void Update()
    {
        if (isTrigger)
        {
            startPosition = transform.localPosition - startPos.localPosition;

            inputDirection = new Vector3(startPosition.x, 0.0f, startPosition.z).normalized;

            Vector3 cameraForward = camera.transform.forward;
            cameraForward.y = 0;

            Vector3 cameraRight = camera.transform.right;
            cameraRight.y = 0;

            moveDirection = cameraForward.normalized * inputDirection.z + cameraRight.normalized * inputDirection.x;
            moveDirection.Normalize();

            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void OnTouch(bool touch)
    {
        if (touch)
            isTrigger = true;
        else
            isTrigger = false;
    }
}
