using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthcolector : MonoBehaviour
{
    public string NameOfObject;
    GameObject Weapon;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Weapon = GameObject.Find(NameOfObject);
            Weapon.GetComponent<HealthPlayer>().currenthealth += 20;
            GameObject.Destroy(gameObject);
        }
    }
}
