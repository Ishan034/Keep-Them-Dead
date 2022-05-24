using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    private float nextTImeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;   

    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float impactForce;
    [SerializeField] private float fireRate;

    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private TMP_Text ammoCounter;
    [SerializeField] private Animator animator;

    [SerializeField] private Camera cam;
    [SerializeField] private ParticleSystem muzzelFlash;
    [SerializeField] private GameObject impactEffect;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTImeToFire)
        {
            nextTImeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        ammoCounter.text = currentAmmo.ToString();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void Shoot()
    {
        muzzelFlash.Play();

        currentAmmo--;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            EnemyTarget target = hit.transform.GetComponent<EnemyTarget>();
            if (target != null)
            {
               target.TakeDamage(damage);  
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObject, 2f);
        }
    }
}
