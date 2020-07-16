using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject go;
    GameObject[] Canvas = GameObject.FindGameObjectsWithTag("Canvas");
    private void Start()
    {
        Time.timeScale = 1;
        go.SetActive(false);
        Canvas = GameObject.FindGameObjectsWithTag("Canvas");
    }
    void Update()
    {        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Mates = GameObject.FindGameObjectsWithTag("Nice Team");
        float EnemysLeft = enemies.Length;
        float MatesLeft = Mates.Length;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(EnemysLeft != 0 && MatesLeft !=0){
                if (go.activeSelf)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

    }
    
    public void PauseGame()
    {
       
        Time.timeScale = 0;
        for(int i = 0; i != Canvas.Length; i++)
        {
            Canvas[i].SetActive(false);
        }
        go.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        for (int i = 0; i != Canvas.Length; i++)
        {
            Canvas[i].SetActive(true);
        }
        go.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
