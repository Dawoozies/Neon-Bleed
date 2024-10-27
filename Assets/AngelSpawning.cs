using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AngelSpawning : MonoBehaviour, IObserver<BossBloodManager>
{
    [Layer] public int defaultLayer;
    public Transform angelSpawnPoint;
    public GameObject angelPrefab;
    public float spawnTime;
    float spawnTimer;
    public float spawnDistance;
    public float spawnVariance;
    public bool spawnAtAwake;
    public bool bossIsActive;
    public ObservedBossBloodManager ObservedBossBloodManager;

    public int OrderPriority => 0;

    public ObservedFloat ObservedPlayerBloodlust;
    public float spawnTimerSpeed;
    public AnimationCurve spawnTimeSpeedCurve;
    private void Awake()
    {
        if(!spawnAtAwake)
            spawnTimer = spawnTime;
    }
    private void OnEnable()
    {
        ObservedBossBloodManager.RegisterObserver(this);
    }
    private void OnDisable()
    {
        ObservedBossBloodManager.UnregisterObserver(this);
    }
    private void Update()
    {
        if (bossIsActive)
            return;

        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime * (1 + spawnTimerSpeed * spawnTimeSpeedCurve.Evaluate(ObservedPlayerBloodlust.GetReference()));
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
    public void OnSetReference(BossBloodManager previousRef, BossBloodManager newRef)
    {
        bossIsActive = newRef != null;
    }
}
