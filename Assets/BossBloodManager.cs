using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossBloodManager : BloodManager
{
    public string bossName;
    public float bleedIntensityHeal;
    public float healTime;
    float healTimer;
    protected override void Update()
    {
        base.Update();

        if(healTimer > 0)
        {
            healTimer -= Time.deltaTime;
        }
        else
        {
            healTimer = healTime;
            if(bleedIntensity > 0f)
            {
                bleedIntensity -= bleedIntensityHeal;
                if(bleedIntensity < 0f)
                {
                    bleedIntensity = 0f;
                }
            }
        }
    }
}