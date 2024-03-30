using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    // Public fields to assign in the inspector
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator; // Make sure to assign this in the Inspector
    public AudioLoudnessDetector detector;
    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;
    public float silenceThreshold = 2f;
    public float patrolRange = 10f;
    public float patrolTime = 5f;
    public float agentSpeedIncreaseFactor = 2f; // Speed increase factor for the NavMeshAgent
    public float animationSpeedIncreaseFactor = 2f; // Speed increase factor for the Animator

    // Private fields used internally by the script
    private float silenceTimer = 0f;
    private bool isChasing = false;
    private float patrolTimer;
    private float originalAgentSpeed; // To store the original agent speed
    private float originalAnimationSpeed; // To store the original animation speed

    void Start()
    {
        // Initialize the patrol timer and store original speeds
        patrolTimer = patrolTime;
        originalAgentSpeed = agent.speed; // Store the original agent speed
        originalAnimationSpeed = animator.speed; // Store the original animation speed
    }

    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness > threshold)
        {
            // Player made a noise, chase the player
            silenceTimer = 0f;
            if (!isChasing)
            {
                isChasing = true;
                agent.speed = originalAgentSpeed * agentSpeedIncreaseFactor; // Increase agent speed
                animator.speed = originalAnimationSpeed * animationSpeedIncreaseFactor; // Increase animation speed
                animator.SetBool("isRunning", true); // Set the animator's running parameter to true
            }
        }
        else
        {
            // No noise, increment the silence timer
            silenceTimer += Time.deltaTime;

            if (isChasing && silenceTimer > silenceThreshold)
            {
                isChasing = false;
                agent.speed = originalAgentSpeed; // Reset agent speed
                animator.speed = originalAnimationSpeed; // Reset animation speed
                animator.SetBool("isRunning", false); // Set the animator's running parameter to false
                patrolTimer = patrolTime; // Reset the patrol timer
            }
        }

        if (isChasing)
        {
            // Set the destination to the player's position
            agent.destination = player.position;
        }
        else
        {
            // Patrol randomly
            Patrol();
        }
    }

    void Patrol()
    {
        // Countdown the patrol timer
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            // Timer has elapsed, time to find a new patrol point
            Vector3 patrolPoint = GetRandomPoint(transform.position, patrolRange);
            agent.SetDestination(patrolPoint);
            patrolTimer = patrolTime; // Reset the patrol timer
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float range)
    {
        // Get a random point on the NavMesh within the range and return it
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        Vector3 finalPoint = Vector3.zero;

        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            finalPoint = hit.position;
        }
        
        return finalPoint;
    }
}
