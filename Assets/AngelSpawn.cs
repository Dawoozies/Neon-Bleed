using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AngelSpawn : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float spawnDistanceThreshold;
    Vector3 moveVelocity;
    float smoothTime;
    public UnityEvent onAngelNotAtSpawnPosition;
    public UnityEvent onAngelAtSpawnPosition;
    bool spawned;
    public void SetSpawnPosition(Vector3 spawnPosition, float smoothTime)
    {
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



        transform.position = Vector3.SmoothDamp(transform.position, spawnPosition, ref moveVelocity, smoothTime);

        if (Vector3.Distance(transform.position, spawnPosition) <= spawnDistanceThreshold)
        {
            spawned = true;
            onAngelAtSpawnPosition?.Invoke();
            transform.position = spawnPosition;
        }
    }
}
