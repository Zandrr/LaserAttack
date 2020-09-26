using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int Playerdamage = 100;
    [SerializeField] int EnemyDamage = 50;

    public int GetPlayerDamage()
    {
        return Playerdamage;
    }

    public int GetEnemyDamage()
    {
        return EnemyDamage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
