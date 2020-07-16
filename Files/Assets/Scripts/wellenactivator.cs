using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wellenactivator : MonoBehaviour
{
    public GameObject EnemysCounter; 
    public GameObject[] WellenManager;
    public GameObject[] GegnerTypen;
    public float Time;
    public int Gegner;
    public int Multiplikator = 2;
    public GameObject Text;
    private float currentWave = 0;
    public float Waves = 3;
    public bool DeathMatch = false;
    private void Update()
    {
        if (!DeathMatch && EnemysCounter.GetComponent<EnemysCounters>().EnemysLeft == 0 && currentWave != Waves)
        {
            int a = UnityEngine.Random.Range(0, WellenManager.Length);
            StartCoroutine(WaveTwo(a));
        }
        if(!DeathMatch){
            Text.GetComponent<Text>().text = currentWave + "/" + Waves + " Wellen";
        }

        if(currentWave == Waves && EnemysCounter.GetComponent<EnemysCounters>().EnemysLeft == 0) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
    private void Start()
    {
        StartCoroutine(Wave());
    }
    IEnumerator Wave()
    {
        for(int i = 0; i != Gegner; i++){
            WellenManager[UnityEngine.Random.Range(0, WellenManager.Length)].GetComponent<WellenManager>().SpawnEnemys(1, GegnerTypen);
        }
        currentWave++;
        Gegner *= Multiplikator;
        yield return new WaitForSeconds(Time);
    }

    IEnumerator WaveTwo(int a)
    {
        WellenManager[UnityEngine.Random.Range(0, WellenManager.Length)].GetComponent<WellenManager>().SpawnEnemys(Gegner, GegnerTypen);
        currentWave++;
        Gegner *= Multiplikator;
        yield return new WaitForSeconds(Time);
    }
}

