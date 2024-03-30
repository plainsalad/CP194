using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lighOB;

    public AudioSource lightSound;

    public float minTime;
    public float maxTime;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LightsFlickering();
    }

    void LightsFlickering()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            lighOB.enabled = !lighOB.enabled;
            lightSound.Play();
            timer = Random.Range(minTime, maxTime);
        }
    }
}
