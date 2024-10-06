using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.MainModule main;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }
    public void Burst()
    {
        ps.Play();
    }
}
