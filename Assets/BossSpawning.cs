using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class BossSpawning : MonoBehaviour
{
    [Layer] public int defaultLayer;
    public Transform spawnPoint;
    public GameObject[] bosses;
    public float spawnTime;
    float spawnTimer;
    int bossIndex;
    public bool spawnAtAwake;
    public ObservedBossBloodManager ActiveBossBloodManager;

    public ObservedFloat ObservedPlayerBloodlust;
    public float spawnTimerSpeed;
    public AnimationCurve spawnTimeSpeedCurve;
    private void Awake()
    {
        ActiveBossBloodManager.SetReference(null);

        if (!spawnAtAwake)
            spawnTimer = spawnTime;
    }
    private void Update()
    {
        if (bossIndex >= bosses.Length)
            return;



        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime * (1 + spawnTimerSpeed* spawnTimeSpeedCurve.Evaluate(ObservedPlayerBloodlust.GetReference()));
        }
        else
        {
            spawnTimer = spawnTime;
            AngelSpawn newAngel = SharedGameObjectPool.Rent(bosses[bossIndex].GetComponent<AngelSpawn>());
            newAngel.gameObject.layer = defaultLayer;
            newAngel.transform.position = spawnPoint.position + Vector3.up;
            newAngel.transform.rotation = Quaternion.identity;
            newAngel.SetSpawnPosition(spawnPoint.position, 0.3f);

            BossBloodManager bossBloodManager = newAngel.GetComponent<BossBloodManager>();
            bossBloodManager.RegisterOnBloodDepletedCallback(OnBossDefeated);
            ActiveBossBloodManager.SetReference(bossBloodManager);

            bossIndex++;
        }
    }
    void OnBossDefeated(BloodManager bloodManager)
    {
        ActiveBossBloodManager.SetReference(null);
    }
}
