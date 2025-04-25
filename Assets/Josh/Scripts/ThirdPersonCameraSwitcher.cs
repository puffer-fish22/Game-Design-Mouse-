using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonCameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera firstPersonCamera; // Assign the Main Camera from XR Origin
    [SerializeField] private Camera thirdPersonCamera; // Assign the ThirdPersonOutputCamera
    [SerializeField] private CinemachineVirtualCamera thirdPersonVCam; // Assign CM VCam ThirdPerson

    [Header("Player Model")]
    [SerializeField] private GameObject playerModelObject; // Assign the RatModel GameObject

    [Header("Input")]
    [SerializeField] private InputActionReference switchCameraAction; // Create and assign an Input Action

    private bool isFirstPerson = true;

    private void Awake()
    {
        // Ensure correct starting state
        SwitchToFirstPerson();

        // Enable the input action and subscribe to its 'performed' event
        switchCameraAction.action.Enable();
        switchCameraAction.action.performed += OnSwitchCameraPressed;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent errors when object is destroyed
        if (switchCameraAction != null && switchCameraAction.action != null)
        {
            switchCameraAction.action.performed -= OnSwitchCameraPressed;
        }
    }

    private void OnSwitchCameraPressed(InputAction.CallbackContext context)
    {
        ToggleCamera();
    }

    public void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;
        if (isFirstPerson)
        {
            SwitchToFirstPerson();
        }
        else
        {
            SwitchToThirdPerson();
        }
    }

    private void SwitchToFirstPerson()
    {
        Debug.Log("Switching to First Person");

        // Enable 1P camera component, disable 3P camera GameObject
        if (firstPersonCamera != null) firstPersonCamera.enabled = true; // Use Camera component enable/disable
        if (thirdPersonCamera != null) thirdPersonCamera.gameObject.SetActive(false); // Disable the whole object is fine here

        // Lower VCam priority so Brain doesn't use it
        if (thirdPersonVCam != null) thirdPersonVCam.Priority = 0;

        // Hide player model
        if (playerModelObject != null) playerModelObject.SetActive(false);

        isFirstPerson = true;
    }

    private void SwitchToThirdPerson()
    {
        Debug.Log("Switching to Third Person");

        // Enable 3P camera GameObject, disable 1P camera component
        if (thirdPersonCamera != null) thirdPersonCamera.gameObject.SetActive(true);
        if (firstPersonCamera != null) firstPersonCamera.enabled = false; // Disable 1P camera component

        // Raise VCam priority so the Brain on the 3P camera picks it up
        if (thirdPersonVCam != null) thirdPersonVCam.Priority = 10; // Higher than default

        // Show player model
        if (playerModelObject != null) playerModelObject.SetActive(true);

        isFirstPerson = false;
    }
}