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
    public bool allowBossRepeat;

    public ObservedInt ActiveAngelCount;
    bool bossActive;
    private void Awake()
    {
        ActiveBossBloodManager.SetReference(null);

        if (!spawnAtAwake)
            spawnTimer = spawnTime;
    }
    private void Update()
    {
        if (paused)
            return;
        if (!allowBossRepeat && bossIndex >= bosses.Length)
            return;

        if (spawnTimer > 0)
        {
            if (bossActive)
                spawnTimer -= Time.deltaTime * (1 + spawnTimerSpeed * spawnTimeSpeedCurve.Evaluate(ObservedPlayerBloodlust.GetReference()));
        }
        else
        {
            spawnTimer = spawnTime;

            if (bossActive)
                StaticData.ins.RandomColorPalette();
        }

        if (ActiveBossBloodManager.GetReference() != null)
            return;


        if (AngelSpawning.ins.angelsDefeated == 2)
        {
            AngelSpawn newAngel = SharedGameObjectPool.Rent(bosses[bossIndex].GetComponent<AngelSpawn>());
            newAngel.gameObject.layer = defaultLayer;
            newAngel.transform.position = spawnPoint.position + Vector3.up;
            newAngel.transform.rotation = Quaternion.identity;
            newAngel.SetSpawnPosition(spawnPoint.position, 0.3f);

            BossBloodManager bossBloodManager = newAngel.GetComponent<BossBloodManager>();
            ActiveAngelCount.Increment();
            bossBloodManager.RegisterOnBloodDepletedCallback(OnBossDefeated);
            ActiveBossBloodManager.SetReference(bossBloodManager);

            bossIndex++;
            if (allowBossRepeat)
            {
                bossIndex = bossIndex % bosses.Length;
            }
            AngelSpawning.ins.angelsDefeated = 0;
            StaticData.ins.RandomColorPalette();
            bossActive = true;
            spawnTimer = spawnTime;
        }
    }
    void OnBossDefeated(BloodManager bloodManager)
    {
        ActiveAngelCount.Decrement();
        ActiveBossBloodManager.SetReference(null);
        bossActive = false;
    }
    public bool paused;
    public void Pause()
    {
        paused = true;
    }
    public void Play()
    {
        paused = false;
    }
}
