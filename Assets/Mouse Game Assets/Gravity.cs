using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
private Rigidbody rb;
private Vector3 objectVelocity;
    // Start is called before the first frame update
    void Start()
    {
                rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
private void Update()
{
rb.AddForce(Vector3.down * 10);
//rb.Move(Vector3.down * 2);

//Vector3 finalMove = (1 * 2) + (objectVelocity.y * Vector3.up);
        //rb.AddForce(finalMove * Time.deltaTime);
}
//if (isMoving)
//groundTrans.Translate(Time.deltaTime * 1 * Vector3.down, 2);
//}
}
