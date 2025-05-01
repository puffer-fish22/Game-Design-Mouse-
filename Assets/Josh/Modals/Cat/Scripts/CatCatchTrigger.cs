using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatCatchTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Caught by the Cat!");
            // You can reload the scene or show UI
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
