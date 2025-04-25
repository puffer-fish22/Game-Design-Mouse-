using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Speed_change : MonoBehaviour
{
[SerializeField] private InputActionReference jumpButton;
[SerializeField] private float jumpHeight = 2.0f;
[SerializeField] private float gravityValue = -9.81f;

private CharacterController _characterController;
private Vector3 _playerVelocity;

private void Start()
    {
        _characterController= GetComponent<CharacterController>();
    }

private void OnTriggerEnter(Collider other){
	//if(other.tag == "Coin"){
	_playerVelocity.x = 0.5f;
	//}
}



}
