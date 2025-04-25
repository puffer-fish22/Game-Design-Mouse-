using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarRigController : MonoBehaviour
{
    [Header("Car Movement")]
    public float speed = 2f;
    public float turnSpeed = 40f;

    [Header("XR Input")]
    public InputActionProperty moveInput; // XRI LeftHand/Move (joystick)

    private void OnEnable()
    {
        moveInput.action.Enable();
    }

    private void OnDisable()
    {
        moveInput.action.Disable();
    }

    private void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        float forward = input.y;
        float turn = input.x;

        // Move forward/backward
        transform.Translate(Vector3.forward * forward * speed * Time.deltaTime);

        // Turn left/right
        transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
    }
}
