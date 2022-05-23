using System;

using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
