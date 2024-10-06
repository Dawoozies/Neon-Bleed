using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxParticles : MonoBehaviour
{
    public float speed;
    ParticleSystem ps;
    ParticleSystem.MainModule main;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }
    private void Update()
    {
        main.simulationSpeed = speed;
        if(Mathf.Approximately(speed, 0f))
        {
            ps.Clear();
        }
    }
}
