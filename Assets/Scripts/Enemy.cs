using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int Health;
    [SerializeField] private float Speed;
    [SerializeField] private int Damage;
    
    // Applies x damage to enemy
    public void EnemyTakeDamage(int dmg)
    {
        Health -= dmg;
    }

    // Returns the damage the enemy gives to the player
    public int GetTakenDamage()
    {
        return Damage;
    }

    public void Update()
    {
        // No health = No alive :)
        if (Health < 0)
        {
            return;
        }
        
    }
}
