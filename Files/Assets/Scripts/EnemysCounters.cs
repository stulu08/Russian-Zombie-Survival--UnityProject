using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemysCounters : MonoBehaviour
{
    public float EnemysLeft = 0;
    public bool enable = true;
    void Update()
    {
        if(enabled){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            EnemysLeft = enemies.Length;
            if (Input.GetKeyDown(KeyCode.A))
            {
                EnemysLeft--;
            }
            if (EnemysLeft == 0)
            {

            }
            gameObject.GetComponent<Text>().text = EnemysLeft + " Enemies Left";
        }
    }
}
