using UnityEngine;
using System.Collections;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float stunTime;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        transform.GetComponent<Enemy>().canMove = false;
        yield return new WaitForSeconds(stunTime);
        transform.GetComponent<Enemy>().canMove = true;
    }
}
