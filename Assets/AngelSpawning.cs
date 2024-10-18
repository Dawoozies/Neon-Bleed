using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AngelSpawning : MonoBehaviour
{
    [Layer] public int defaultLayer;
    public Transform angelSpawnPoint;
    public GameObject angelPrefab;
    public float spawnTime;
    float spawnTimer;
    public float spawnDistance;
    public float spawnVariance;
    private void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = spawnTime;
            AngelSpawn newAngel = SharedGameObjectPool.Rent(angelPrefab.GetComponent<AngelSpawn>());
            newAngel.gameObject.layer = defaultLayer;
            newAngel.transform.position = angelSpawnPoint.position + Vector3.up * spawnDistance + (Vector3)Random.insideUnitCircle * spawnVariance;
            newAngel.transform.rotation = Quaternion.identity;
            newAngel.SetSpawnPosition(angelSpawnPoint.position, 0.3f);
        }
    }
}
