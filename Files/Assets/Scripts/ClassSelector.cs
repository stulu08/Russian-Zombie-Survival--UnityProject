using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelector : MonoBehaviour
{
    public GameObject Assault;
    public GameObject AssaultSpawn;

    public GameObject Shotgun;
    public GameObject ShotGunSpawn;

    public GameObject Heavy;
    public GameObject HeavySpawn;

    public void ButtonClickAssault()
    {
        Instantiate(Assault, AssaultSpawn.transform.position, Quaternion.identity);
        gameObject.GetComponent<DeathMatchStart>().StartGame();
    }

    public void ButtonClickShotGun()
    {
        Instantiate(Shotgun, ShotGunSpawn.transform.position, Quaternion.identity);
        gameObject.GetComponent<DeathMatchStart>().StartGame();
    }

    public void ButtonClickHeavy()
    {
        Instantiate(Heavy, HeavySpawn.transform.position, Quaternion.identity);
        gameObject.GetComponent<DeathMatchStart>().StartGame();
    }
}

