using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowFromMicrophone : MonoBehaviour
{
    // public Vector3 minScale, maxScale;
    public AudioLoudnessDetector detector;

    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;
    public float silenceThreshold = 2f;

    public Transform player;

    private NavMeshAgent agent;

    private float silenceTimer = 0f;
    private bool isChasing = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        // Debug.Log(loudness);
        if (loudness > threshold)
        {
            // Player is making noise
            silenceTimer = 0f; // Reset silence timer

            if (!isChasing)
            {
                // Start chasing
                isChasing = true;
                // agent.destination = player.position;
            }
        }
        else
        {
            // Player is silent
            silenceTimer += Time.deltaTime;

            if (isChasing && silenceTimer > silenceThreshold)
            {
                // Stop chasing after remaining silent for a certain duration
                isChasing = false;
                agent.destination = transform.position; // Stop the enemy
            }
        }
        if (isChasing)
        {
            agent.destination = player.position;
        }
        // if (loudness < threshold)
        // {
        //     loudness = 0;
        // }
        // transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
