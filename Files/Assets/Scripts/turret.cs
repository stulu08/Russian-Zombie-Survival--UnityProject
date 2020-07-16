using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class turret : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float hitforce = 30f;
    public float nextTimeToFire = 0f;
    public NavMeshAgent NavMesh;
    public GameObject Tower;
    private GameObject Enemy;
    public Camera fpsCam;
    public ParticleSystem muzzelFlash;
    public GameObject hitpoint;
    public float radius = 15;

    private void Start()
    {
        NavMesh = gameObject.GetComponent<NavMeshAgent>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        

        if (Vector3.Distance(Enemy.transform.position, transform.position) < radius && Time.time >= nextTimeToFire) 
        { 
            Tower.transform.LookAt(Enemy.transform.position);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }
    void Shoot()
    {
        muzzelFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitforce);
            }
            GameObject impactGO = Instantiate(hitpoint, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
