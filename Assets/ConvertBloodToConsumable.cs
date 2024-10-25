using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class ConvertBloodToConsumable : MonoBehaviour
{
    public GameObject drinkableBloodPrefab;
    public float spawnRadius;
    public int bloodCount;
    //AsyncObjectPool<Rigidbody2D> blood
    void OnParticleCollision(GameObject other)
    {
        GameObject blood = SharedGameObjectPool.Rent(drinkableBloodPrefab);
        Vector2 bloodPos = new Vector2(other.transform.position.x, transform.position.y);
        blood.transform.position = bloodPos + Random.insideUnitCircle* spawnRadius;
    }
}
