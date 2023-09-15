using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick_3D_2 : MonoBehaviour
{
    [SerializeField] Transform strartPos, camera;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 2.5f;

    private Vector3 strartPosition, inputDirection, moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        strartPosition = transform.localPosition - strartPos.localPosition;

        inputDirection = new Vector3(strartPosition.x, 0, -strartPosition.y);

        Vector3 cameraForward = camera.transform.forward;
        cameraForward.y = 0;

        Vector3 cameraRight = camera.transform.right;
        cameraRight.y = 0;

        moveDirection = cameraForward.normalized * inputDirection.z + cameraRight.normalized * inputDirection.x;
        moveDirection.Normalize();

        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}
