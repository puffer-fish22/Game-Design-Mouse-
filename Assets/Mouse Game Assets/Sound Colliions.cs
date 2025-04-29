using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollisions : MonoBehaviour
{

//public Transform target;
private Rigidbody rb;
private Collider collider;
    private AudioSource source;
    public AudioClip Hit_Sound1;
    public AudioClip Hit_Sound2;
public float IsCollisionHappening = 0;



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
	IsCollisionHappening = 0;

        
    }

//
   //
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Real Object"){

			GameObject otherObj = collision.gameObject;
			Debug.Log("Collided with: " + otherObj);
			source.PlayOneShot(Hit_Sound1);
			IsCollisionHappening = 1;
			collider.isTrigger = true;
		}
		else {
		IsCollisionHappening = 0;
		}
    }

	//void OnCollisionExit(Collision collision) {
	//	GameObject otherObj = collision.gameObject;
	//	Debug.Log("Collided with: " + otherObj);
	//	source.PlayOneShot(Hit_Sound2);
	//	IsCollisionHappening = 0;
    //}

	void OnTriggerEnter(Collider collider) {
		GameObject otherObj = collider.gameObject;
		Debug.Log("Triggered with: " + otherObj);
		source.PlayOneShot(Hit_Sound2);
    }

}

