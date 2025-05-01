using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAI : MonoBehaviour
{
    public enum CatState { Patrol, Chase }
    public CatState currentState = CatState.Patrol;

    public Transform player;
    public float sightRange = 8f;
    public float catchDistance = 1f;
    public float loseSightTime = 5f;

    private float loseSightTimer = 0f;

    public CatAudioController catAudio;

    [Header("")]
    public Transform[] patrolPoints;

    private int currentPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();

        // Trigger patrol audio at start
        if (catAudio) catAudio.OnStartPatrol();
    }

    void Update()
    {
        switch (currentState)
        {
            case CatState.Patrol:
                PatrolUpdate();
                LookForPlayer();
                break;

            case CatState.Chase:
                ChaseUpdate();
                break;
        }
    }

    void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void ChaseUpdate()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // PLAYER ESCAPES
            if (distanceToPlayer > sightRange)
            {
                loseSightTimer += Time.deltaTime;
                if (loseSightTimer >= loseSightTime)
                {
                    loseSightTimer = 0f;
                    currentState = CatState.Patrol;
                    GoToNextPatrolPoint();

                    if (catAudio) catAudio.OnEscaped();
                }
            }
            else
            {
                loseSightTimer = 0f;
            }

            //PLAYER CAUGHT
            if (distanceToPlayer < catchDistance)
            {
                FindObjectOfType<EvasionMission>().HandleCaught();

                // Audio: Play caught scream
                if (catAudio) catAudio.OnCaught();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, catchDistance);
    }
    

    void LookForPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= sightRange && currentState == CatState.Patrol)
        {
            currentState = CatState.Chase;

            //Audio: Play chase stinger + pitch up loop
            if (catAudio) catAudio.OnStartChase();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}
