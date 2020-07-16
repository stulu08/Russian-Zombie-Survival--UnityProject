using JetBrains.Annotations;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    private NavMeshAgent NavMesh;
    private GameObject player;
    private float MaxHealth = 50f;
    private float Regenetarion = 1f;
    public float health = 50f;
    public bool playAnimOnDeath = false;
    public GameObject destroyedVersion;
    public Animator Animator;
    public float DeathTime = 4f;
    public float AttackDamage = 20f;
    public float AttackAnimDuration = 2f;
    public float AttackFirstHit = 1f;
    public Slider HealthBar;
    [HideInInspector]
    public GameObject ZombieBase;
    [Header("Loot")]
    public bool EnableLoot = false;
    public GameObject[] Objects;
    public Transform LootSpawner;
    bool isdead = false;


    public void TakeDamage (float amount)
    {
        Animator.SetTrigger("damage");
        health -= amount;
        
        if(health <= 0f)
        {
            Destroy(HealthBar);
            Die();
        }
            
    }
    IEnumerator SpawnLoot()
    {
        var number = Random.Range(0, Objects.Length);
        Instantiate(Objects[number], LootSpawner.position, Quaternion.identity);
        yield return new WaitForSeconds(DeathTime);
        
    }
    void Start()
    {
        health = MaxHealth;
        HealthBar.maxValue = MaxHealth;

        NavMesh = gameObject.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Nice Team");

    }
    public void Die()
    {
        gameObject.tag = "Dead";
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Destroy(HealthBar);
        if (!playAnimOnDeath){
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            if(!isdead){
                StartCoroutine(SpawnLoot());
                isdead = true;
            }
            Animator.SetTrigger("death");
            Destroy(gameObject, DeathTime);
        }
       
    }
    private void LateUpdate()
    {
        ZombieBase = GameObject.FindGameObjectWithTag("ZombieBase");
    }
    private void Update()
    {
        if (health >= 10)
            Folowplayer();
        else
            EscapePlayer();

        Health();
        player = GameObject.FindGameObjectWithTag("Nice Team");

    }
    void Health()
    {
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = health;
        if(health < MaxHealth)
        {
            health += Regenetarion * Time.deltaTime;
        }
        if(health > MaxHealth)
        {
            health = MaxHealth;
        }
    }
    void EscapePlayer()
    {
        NavMesh.destination = new Vector3(ZombieBase.transform.position.x, ZombieBase.transform.position.y, ZombieBase.transform.position.z);
        Animator.SetFloat("speed", NavMesh.speed);
        
    }
    void Folowplayer()
    {
        NavMesh.destination = player.transform.position;
        Animator.SetFloat("speed", NavMesh.speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(health >= 10){
            if (collision.collider.tag == "Nice Team" || collision.collider.tag == "Player"){
                GameObject Target = collision.collider.gameObject;
                StartCoroutine(Attack(AttackDamage, Target));
            }
        }
        if(collision.collider.tag == "EnemyHealPad")
        {
            health = MaxHealth;
        }
    }
    IEnumerator Attack(float Damage, GameObject Target)
    {
        HealthPlayer target = Target.GetComponent<HealthPlayer>();
        AttackAnimDuration -= AttackFirstHit;
        Animator.SetTrigger("attack");
        new WaitForSeconds(AttackFirstHit);
        target.TakeDamage(Damage);
        new WaitForSeconds(AttackAnimDuration);
        yield return null;
    }

}

