using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float hitforce = 30f;
    public float nextTimeToFire = 0f;
    public bool atomatic = true;
    public float reloadTime = 1f;
    public float switchtimereload = 0.25f;

    public int maxAmmo = 10;
    public int collectetAmmo = 100;
    public int currentAmmo;
    public Camera fpsCam;
    public ParticleSystem muzzelFlash;
    public Animator Animator;
    public GameObject hitpoint;

    public Text AmmoText;
    public bool Shotgun = false;
    private bool isreloading = false;
    private bool reloadtwo = false;
    private void Start()
    {
        if(currentAmmo == -1)
        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isreloading = false;
        Animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if (gameObject.activeSelf)
        {
            AmmoText.text = currentAmmo + "/" + maxAmmo + " / " + collectetAmmo;
        }

        if (isreloading)
            return;
        if(currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (collectetAmmo >= maxAmmo) 
            { 
                StartCoroutine(Reload());
                return;
            }
            else if(collectetAmmo >= 1)
            {
                StartCoroutine(Reload());
                reloadtwo = true;
                return;
            }
            
        }
        if(collectetAmmo <= -1)
        {
            collectetAmmo = 0;
        }
       
       if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !atomatic && currentAmmo >= 1f)
        {
            if (Shotgun)
            {
                currentAmmo--;
                int amountOfProjectiles = 4;
                    for (int i = 0; i < amountOfProjectiles; i++)
                    {
                        Vector3 direction = fpsCam.transform.forward;
                        Vector3 spread = fpsCam.transform.position;
                        spread += fpsCam.transform.up * Random.Range(-0.03f, 0.3f);
                        spread += fpsCam.transform.right * Random.Range(-0.3f, 0.3f);
                        direction += spread * Random.Range(0f, 0.0f);

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
            }
            else{
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                ShootAtTaget();
            }


        }
        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && atomatic && currentAmmo >= 1f)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            ShootAtTaget();


        }

    }
    IEnumerator Reload()
    {
        isreloading = true;
        Debug.Log("Reloading...");
        Animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime -switchtimereload);
        Animator.SetBool("Reloading", false);
        isreloading = false;
        if(currentAmmo <= 1 && !reloadtwo){
            currentAmmo = maxAmmo;
            collectetAmmo -= maxAmmo;
        }
        else if (reloadtwo)
        {
            int curr = collectetAmmo;
            collectetAmmo -= maxAmmo - currentAmmo;
            currentAmmo = curr;
            reloadtwo = false;
        }
        else
        {
            collectetAmmo -= maxAmmo - currentAmmo;
            currentAmmo = maxAmmo;
        }
        
    }
    void Shoot()
    {
        muzzelFlash.Play();
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           

            Enemy target = hit.transform.GetComponent<Enemy>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitforce);
            }
            GameObject impactGO = Instantiate(hitpoint, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Triggerer")
        {
            collectetAmmo += 2;
        }
    }

    void ShootAtTaget()
    {
        muzzelFlash.Play();
        currentAmmo--;
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
}