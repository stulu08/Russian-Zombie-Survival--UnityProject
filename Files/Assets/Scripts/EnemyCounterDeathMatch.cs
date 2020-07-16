using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyCounterDeathMatch : MonoBehaviour
{
    public float EnemysLeft = 0;
    public float MatesLeft = 0;
    public GameObject text;
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Mates = GameObject.FindGameObjectsWithTag("Nice Team");
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        EnemysLeft = enemies.Length ;
        MatesLeft = Mates.Length + Players.Length;
        if (EnemysLeft == 0)
        {
            SceneManager.LoadScene(2);
        }
        else if(MatesLeft == 0)
        {
            SceneManager.LoadScene(1);
        }
        text.GetComponent<Text>().text = MatesLeft + " vs " + EnemysLeft;
    }
}
