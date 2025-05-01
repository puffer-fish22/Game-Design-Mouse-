using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvasionMission : MonoBehaviour
{
    public CatAI catAI;
    public TextMeshProUGUI statusText;

    public float evadeMessageDuration = 3f;
    public float caughtRestartDelay = 2f;

    private float evadeMessageTimer = 0f;
    private bool caught = false;

    private CatAI.CatState lastState;

    void Start()
    {
        lastState = catAI.currentState;
        statusText.text = "";
    }

    void Update()
    {
        if (caught) return;

        // Check if cat just started chasing
        if (catAI.currentState == CatAI.CatState.Chase && lastState != CatAI.CatState.Chase)
        {
            statusText.text = "Escape the cat!";
         
        }

        // Check if cat just stopped chasing (escaped)
        if (catAI.currentState == CatAI.CatState.Patrol && lastState == CatAI.CatState.Chase)
        {
            statusText.text = "You have evaded the cat!";
   

            evadeMessageTimer = evadeMessageDuration;
        }

        // Countdown to clear "evaded" message
        if (evadeMessageTimer > 0f)
        {
            evadeMessageTimer -= Time.deltaTime;
            if (evadeMessageTimer <= 0f)
            {
                statusText.text = "";
            }
        }

        lastState = catAI.currentState;
    }

    public void HandleCaught()
    {
        if (caught) return;

        caught = true;
        statusText.text = "You have been caught!";
      

        Invoke(nameof(RestartGame), caughtRestartDelay);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
