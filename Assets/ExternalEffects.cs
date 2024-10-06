using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEffects : MonoBehaviour
{
    public static ExternalEffects ins;
    ParticleSystem[] particleSystems;
    private void Awake()
    {
        ins = this;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }
    public void PlayEffect(int effectIndex, Vector3 pos)
    {
        if (effectIndex >= particleSystems.Length)
            return;
        particleSystems[effectIndex].transform.position = pos;
        particleSystems[effectIndex].Play();
    }
}
