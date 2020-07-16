using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammocollector : MonoBehaviour
{
    public string NameOfWeapon;
    public int Ammo = 30;
    GameObject Weapon;
    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Nice Team")
        {
            Weapon = GameObject.Find("Player/MainCamera/WeaponHolder/" + NameOfWeapon);
            if(Weapon.activeSelf){
                Weapon.GetComponent<Gun>().collectetAmmo += Ammo;
                GameObject.Destroy(gameObject);
            }
        }
    }
}
