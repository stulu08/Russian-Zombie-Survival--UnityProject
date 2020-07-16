using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchStart : MonoBehaviour
{
    public GameObject[] ObjectsToenable;
    public GameObject OwnCanvas;
    void Start()
    {
        OwnCanvas.SetActive(true);
        for(int i = 0; i != ObjectsToenable.Length; i++)
        {
            ObjectsToenable[i].SetActive(false);
        }
        
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        OwnCanvas.SetActive(false);
        for (int i = 0; i != ObjectsToenable.Length; i++)
        {
            ObjectsToenable[i].SetActive(true);
        }

        Time.timeScale = 1;
    }
}
