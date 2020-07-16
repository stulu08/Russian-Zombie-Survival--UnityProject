using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class WellenManager : MonoBehaviour
{
    public void SpawnEnemys(int Anzahl, GameObject[] GegnerTypen)
    {
        int a = GegnerTypen.Length;
        for (int i = 0; i < Anzahl; i++)
            {
                Instantiate(GegnerTypen[Random.Range(0, a)], gameObject.transform.position, Quaternion.identity);
            }
    }
}
