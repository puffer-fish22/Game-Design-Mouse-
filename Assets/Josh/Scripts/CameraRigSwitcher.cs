using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRigSwitcher : MonoBehaviour
{
    [Header("XR Rigs")]
    public GameObject playerRig;
    public GameObject carRig;
    public GameObject driveableCar;

    [Header("Input")]
    public InputActionProperty enterCarAction;
    public InputActionProperty exitCarAction;

    [Header("Trigger")]
    public Collider entryZone;
    private bool isPlayerNearCar = false;
    private bool inCar = false;

    void OnEnable()
    {
        enterCarAction.action.Enable();
        exitCarAction.action.Enable();

        enterCarAction.action.performed += HandleEnterCar;
        exitCarAction.action.performed += HandleExitCar;
    }

    void OnDisable()
    {
        enterCarAction.action.performed -= HandleEnterCar;
        exitCarAction.action.performed -= HandleExitCar;

        enterCarAction.action.Disable();
        exitCarAction.action.Disable();
    }

    private void HandleEnterCar(InputAction.CallbackContext ctx)
    {
        if (inCar || !isPlayerNearCar) return;

        playerRig.SetActive(false);
        carRig.SetActive(true);

        inCar = true;
        driveableCar.GetComponent<CarRigController>().enabled = true;

        Debug.Log("Entered car rig");

    }

    private void HandleExitCar(InputAction.CallbackContext ctx)
    {
        if (!inCar) return;

        carRig.SetActive(false);
        playerRig.SetActive(true);

        inCar = false;
        driveableCar.GetComponent<CarRigController>().enabled = false;

        Debug.Log("Exited car rig");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in Trigger Zone");
            isPlayerNearCar = true;
        }

        Debug.Log("In Trigger Zone");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearCar = false;
    }
}
