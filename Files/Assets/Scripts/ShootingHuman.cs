using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class ShootingHuman : MonoBehaviour
{
    public float damage = 10f;
    public float detectRange = 40f;
    public float range = 30f;
    public float fireRate = 15f;
    public float hitforce = 30f;
    public float nextTimeToFire = 0f;
    public NavMeshAgent NavMesh;
    public GameObject Head;
    private GameObject Enemy;
    public Camera fpsCam;
    public ParticleSystem muzzelFlash;
    public GameObject hitpoint;
    public float radius = 20;
    public string TargetTag = "Enemy/Player";
    public bool attackplayer = true;
    private void Start()
    { 
        NavMesh = gameObject.GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        
        Enemy = GameObject.FindGameObjectWithTag(TargetTag);
        GameObject[] Player= GameObject.FindGameObjectsWithTag("Player");

        if (Player.Length != 0 &&Vector3.Distance(Player[0].transform.position, transform.position) < range - 5 && Time.time >= nextTimeToFire && attackplayer)
        {
            NavMesh.destination = Player[0].transform.position;
            Head.transform.LookAt(Player[0].transform.position);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            ShootTwo();
        }
        else
        {
            NavMesh.destination = Enemy.transform.position;
            Head.transform.LookAt(Enemy.transform.position);
            if (Vector3.Distance(Enemy.transform.position, transform.position) < range)
            {
               
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                ShootTwo();
            }
            
        }

        /*if (Vector3.Distance(Enemy.transform.position, transform.position) < detectRange && Enemy.GetComponent<PlayerMovement>())
        {
            NavMesh.destination = Enemy.transform.position;
        } 
        else if (Vector3.Distance(Enemy.transform.position, transform.position) < detectRange)
        {
            NavMesh.destination = Enemy.transform.position;
            Head.transform.LookAt(Enemy.transform.position);
        }


        if (Vector3.Distance(Enemy.transform.position, transform.position) < radius && Enemy.GetComponent<PlayerMovement>() && Time.time >= nextTimeToFire)
        {
            NavMesh.destination = new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, Enemy.transform.position.z);
            Head.transform.LookAt(Enemy.transform.position);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }


        if (Vector3.Distance(Enemy.transform.position, transform.position) < detectRange && Enemy.GetComponent<Enemy>() && Time.time >= nextTimeToFire)
        {
            NavMesh.destination = Enemy.transform.position;
        }


        if (Vector3.Distance(Enemy.transform.position, transform.position) < radius && Enemy.GetComponent<Enemy>() && Time.time >= nextTimeToFire)
        {
            NavMesh.destination = Enemy.transform.position;
            Head.transform.LookAt(Enemy.transform.position);
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }


       
        else if (Vector3.Distance(Enemy.transform.position, transform.position) < radius && Time.time >= nextTimeToFire)
        {
            NavMesh.destination = Enemy.transform.position;
            Head.transform.LookAt(Enemy.transform.position);
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootTwo();
        }
        */
    }
    void Shoot()
    {
        muzzelFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitforce);
            }
            GameObject impactGO = Instantiate(hitpoint, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void ShootTwo()
    {
        muzzelFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            HealthPlayer target = hit.transform.GetComponent<HealthPlayer>();
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            gameObject.GetComponent<HealthPlayer>().TakeDamage(Enemy.GetComponent<Enemy>().AttackDamage);
        }
    }
}
