using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandPresencePhysics : MonoBehaviour
{

public Transform target;
private Rigidbody rb;
//
public Renderer nonPhysicalHand;
public float showNonPhysicalHandDistance = 0.2f;
//
private Collider[] handColliders;
//
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
	handColliders = GetComponentsInChildren<Collider>();
	
    }
//
public void EnableHandCollider()
{
	foreach (var item in handColliders)
	{
		item.enabled = true;
	}
}

public void EnableHandColliderDelay(float delay)
{
	Invoke("EnableHandCollider", delay);
}

public void DisableHandCollider()
{
	foreach (var item in handColliders)
	{
		item.enabled = false;
	}
}

//
    private void Update()
    {
	float distance = Vector3.Distance(transform.position, target.position);

	if(distance>showNonPhysicalHandDistance)
	{
		nonPhysicalHand.enabled = true;
	}
	else
		nonPhysicalHand.enabled = false;
		//nonPhysicalHand.enabled = true;
	}

//
    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

	Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
	rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

	Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

	rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }


}
