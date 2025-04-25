using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class VehicleController : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin; // XR Origin (formerly XR Rig)
    public Transform driverSeatPosition; // Empty GameObject positioned at driver's seat
    public Rigidbody carRigidbody; // The car's rigidbody

    [Header("Vehicle Settings")]
    public float accelerationForce = 1000f;
    public float turnSpeed = 100f;

    [Header("Input Actions")]
    public InputActionProperty enterExitAction; // Reference to button press action (A button)
    public InputActionProperty steeringAction; // Reference to joystick/thumbstick action

    // State tracking
    private bool isPlayerInCar = false;
    private Vector3 originalXROriginPosition;
    private Quaternion originalXROriginRotation;
    private CharacterController characterController;
    private ActionBasedContinuousMoveProvider moveProvider;
    private bool buttonWasPressed = false;
    private float buttonCooldown = 0f;

    void Awake()
    {
        // Find components if not assigned
        if (xrOrigin == null)
            xrOrigin = FindObjectOfType<XROrigin>();

        if (carRigidbody == null)
            carRigidbody = GetComponent<Rigidbody>();

        // Find movement components
        characterController = xrOrigin.GetComponentInChildren<CharacterController>();
        moveProvider = xrOrigin.GetComponentInChildren<ActionBasedContinuousMoveProvider>();
    }

    void OnEnable()
    {
        // Enable input actions
        enterExitAction.action.Enable();
        steeringAction.action.Enable();
    }

    void OnDisable()
    {
        // Disable input actions
        enterExitAction.action.Disable();
        steeringAction.action.Disable();
    }

    void Update()
    {
        // Cooldown for button presses to prevent double triggers
        if (buttonCooldown > 0)
        {
            buttonCooldown -= Time.deltaTime;
        }

        // Check for enter/exit button press
        bool buttonPressed = enterExitAction.action.ReadValue<float>() > 0.5f;

        // Detect button press (only on press, not hold)
        if (buttonPressed && !buttonWasPressed && buttonCooldown <= 0)
        {
            if (!isPlayerInCar)
            {
                EnterCar();
            }
            else
            {
                ExitCar();
            }

            buttonCooldown = 0.5f; // Set cooldown to prevent accidental double-presses
        }

        buttonWasPressed = buttonPressed;

        // Handle car driving when player is in car
        if (isPlayerInCar)
        {
            // Get input from thumbstick/joystick
            Vector2 steeringInput = steeringAction.action.ReadValue<Vector2>();

            // Forward/backward acceleration
            float acceleration = steeringInput.y * accelerationForce;
            carRigidbody.AddForce(transform.forward * acceleration * Time.deltaTime);

            // Turning left/right
            float rotation = steeringInput.x * turnSpeed * Time.deltaTime;
            transform.Rotate(0f, rotation, 0f);
        }
    }

    void EnterCar()
    {
        isPlayerInCar = true;

        // Store original position/rotation
        originalXROriginPosition = xrOrigin.transform.position;
        originalXROriginRotation = xrOrigin.transform.rotation;

        // Disable standard movement components
        if (characterController != null)
            characterController.enabled = false;
        if (moveProvider != null)
            moveProvider.enabled = false;

        // Position player in driver's seat
        Camera mainCamera = xrOrigin.Camera;
        if (mainCamera != null && driverSeatPosition != null)
        {
            // Calculate offset between camera and XR origin
            Vector3 cameraOffset = mainCamera.transform.position - xrOrigin.transform.position;

            // Position XR origin so camera is at driver seat
            xrOrigin.transform.position = driverSeatPosition.position - cameraOffset;
        }

        Debug.Log("Entered car");
    }

    void ExitCar()
    {
        isPlayerInCar = false;

        // Position player outside car
        xrOrigin.transform.position = transform.position + transform.right * 2f; // 2 units to the right of car

        // Re-enable standard movement
        if (characterController != null)
            characterController.enabled = true;
        if (moveProvider != null)
            moveProvider.enabled = true;

        Debug.Log("Exited car");
    }

    void LateUpdate()
    {
        // If player is in car, update XR origin position to match car
        if (isPlayerInCar && driverSeatPosition != null)
        {
            // Calculate where driver position is now
            Camera mainCamera = xrOrigin.Camera;
            if (mainCamera != null)
            {
                // Calculate offset between camera and XR origin
                Vector3 cameraOffset = mainCamera.transform.position - xrOrigin.transform.position;

                // Position XR origin so camera is at driver seat
                xrOrigin.transform.position = driverSeatPosition.position - cameraOffset;
            }
        }
    }
}