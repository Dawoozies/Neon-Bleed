using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBloodManager : BloodManager
{
    public float bleedIntensityHeal;
    public float healTime;
    float healTimer;
    //
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
