using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]

public class Jump : MonoBehaviour
{

[SerializeField] private InputActionReference jumpActionReference;
[SerializeField] private float jumpForce = 50.0f;

private Rigidbody _body;
//private CharacterController controller;

private bool IsGrounded => Physics.Raycast(
	new Vector2(transform.position.x, transform.position.y + 1.0f),
Vector3.down, 2.0f);

    // Start is called before the first frame update
    void Start()
    {

        _body = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
	jumpActionReference.action.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {

    }

private void OnJump(InputAction.CallbackContext obj)
{
	if (!IsGrounded) return;
	_body.AddForce(Vector3.up * jumpForce);
	//_body.Move(Vector3.up * jumpForce);
	}



}