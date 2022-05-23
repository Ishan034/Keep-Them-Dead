using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [SerializeField] private Camera cam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyTarget enemyTarget = hit.transform.GetComponent<EnemyTarget>();
            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damage);
            }
        }
    }
}
