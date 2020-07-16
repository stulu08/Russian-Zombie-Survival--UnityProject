using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthPlayer : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float currenthealth;
    public float Regeneration = 1f;
    public Text Text;
    public Slider Slider;
    public bool WithSlider = true;
    public bool isPlayer = true;
    public bool DeathMatchPlayerDeath = false;
    public GameObject CanvasDeathmatch;
    private bool damaged = false;
    public void TakeDamage(float ammount)
    {
        currenthealth -= ammount;
        if(currenthealth <= 0f)
        {
            
            if (isPlayer)
            {
                Die();
            }
            else
            {
                enemyDie();
            }
        }
    }
    private void Start()
    {
        currenthealth = MaxHealth;
        if (DeathMatchPlayerDeath)
        {
            isPlayer = false;
        }
    }
    void Update()
    {
        if (DeathMatchPlayerDeath)
        {
            isPlayer = false;
        }
        if (WithSlider){
            if(currenthealth < 0)
            {
                Text.text = "0Leben";
            }
            else
                Text.text = Mathf.Floor(currenthealth) + "Leben";
            Slider.maxValue = MaxHealth;
            Slider.value = currenthealth;
        }
        
        if(currenthealth <= MaxHealth && !damaged)
        {
            currenthealth += Regeneration * Time.deltaTime;
        }
        if(currenthealth >= MaxHealth)
        {
            currenthealth = MaxHealth;
        }
        if (currenthealth < 1f)
        {

            if (isPlayer)
            {
                Die();
            }
            else
            {
                enemyDie();
            }
        }
    }
    void Die()
    {
        SceneManager.LoadScene(1);
    }
    void enemyDie()
    {
        if (DeathMatchPlayerDeath)
        {
            Destroy(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    IEnumerator damagetime()
    {
        damaged = true;
        new WaitForSeconds(3f);
        damaged = false;
        yield return null;
    }
}
