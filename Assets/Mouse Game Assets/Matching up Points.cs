using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingupPoints : MonoBehaviour
{
[SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Cube2.transform.SetParent( Cube1.transform);
 	Cube2.transform.localPosition = Vector3.zero;
 	Cube2.transform.localRotation = Quaternion.Identity;
 	Cube2.transform.localScale = Vector3.one;
 	Cube2.transform.SetParent(null);  
    }
}
