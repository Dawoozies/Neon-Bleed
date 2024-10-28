using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class AngelSpawn : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float spawnDistanceThreshold;
    Vector3 moveVelocity;
    float smoothTime;
    public UnityEvent onAngelNotAtSpawnPosition;
    public UnityEvent onAngelAtSpawnPosition;
    bool spawned;
    public float distance;
    public void SetSpawnPosition(Vector3 spawnPosition, float smoothTime)
    {
        Debug.Log("SetSpawnPosition running");
        this.spawnPosition = spawnPosition;
        this.smoothTime = smoothTime;
        spawned = false;

        onAngelNotAtSpawnPosition?.Invoke();
    }
    private void Update()
    {
        if(spawned)
        {
            return;
        }

        distance = Vector3.Distance(transform.position, spawnPosition);
        if (Vector3.Distance(transform.position, spawnPosition) <= spawnDistanceThreshold)
        {
            spawned = true;
            onAngelAtSpawnPosition?.Invoke();
            transform.position = spawnPosition;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, spawnPosition, ref moveVelocity, smoothTime);
        }
    }
}
