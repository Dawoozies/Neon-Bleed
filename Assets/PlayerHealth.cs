using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public UnityEvent onPlayerDead;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            onPlayerDead?.Invoke();
        }
    }
}
