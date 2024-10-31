using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AngelSpawning : MonoBehaviour, IObserver<BossBloodManager>
{
    [Layer] public int defaultLayer;
    public Transform angelSpawnPoint;
    public GameObject[] angelPrefabs;
    public float spawnTime;
    float spawnTimer;
    public float spawnDistance;
    public float spawnVariance;
    public bool paused;
    public bool spawnAtAwake;
    public bool bossIsActive;
    public ObservedBossBloodManager ObservedBossBloodManager;

    public int OrderPriority => 0;

    public ObservedFloat ObservedPlayerBloodlust;
    public float spawnTimerSpeed;
    public AnimationCurve spawnTimeSpeedCurve;

    public int maxAngelsAtOnce;
    public ObservedInt ActiveAngelCount;
    private void Awake()
    {
        if(!spawnAtAwake)
            spawnTimer = spawnTime;

        ActiveAngelCount.SetReference(0);
    }
    private void OnEnable()
    {
        ObservedBossBloodManager.RegisterObserver(this);
    }
    private void OnDisable()
    {
        ObservedBossBloodManager.UnregisterObserver(this);
    }
    public GameObject GetRandomAngel()
    {
        return angelPrefabs[Random.Range(0, angelPrefabs.Length)];
    }
    private void Update()
    {
        if (paused)
            return;
        if (bossIsActive)
            return;

        if (ActiveAngelCount.GetReference() >= maxAngelsAtOnce)
            return;

        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime * (1 + spawnTimerSpeed * spawnTimeSpeedCurve.Evaluate(ObservedPlayerBloodlust.GetReference()));
        }
        else
        {
            spawnTimer = spawnTime;
            AngelSpawn newAngel = SharedGameObjectPool.Rent(GetRandomAngel().GetComponent<AngelSpawn>());
            newAngel.gameObject.layer = defaultLayer;
            newAngel.transform.position = angelSpawnPoint.position + Vector3.up * spawnDistance + (Vector3)Random.insideUnitCircle * spawnVariance;
            newAngel.transform.rotation = Quaternion.identity;
            newAngel.SetSpawnPosition(angelSpawnPoint.position, 0.3f);

            BloodManager bloodManager = newAngel.GetComponent<BloodManager>();
            ActiveAngelCount.Increment();
            bloodManager.RegisterOnBloodDepletedCallback(OnAngelDefeated);
        }
    }
    public void OnSetReference(BossBloodManager previousRef, BossBloodManager newRef)
    {
        bossIsActive = newRef != null;
    }
    void OnAngelDefeated(BloodManager bloodManager)
    {
        ActiveAngelCount.Decrement();
    }
    public void Pause()
    {
        paused = true;
    }
    public void Play()
    {
        paused = false;
    }
}
