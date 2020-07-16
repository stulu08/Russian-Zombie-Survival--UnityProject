using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    
    public int selectetWeapon = 0;

    void Start()
    {
        SelectetWeapon();
    }

    void Update()
    {
        int previusSelectedWeapon = selectetWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectetWeapon >= transform.childCount - 1)
                selectetWeapon = 0;
            else
                selectetWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectetWeapon <= 0)
                selectetWeapon = transform.childCount -1;
            else
                selectetWeapon--;
        }
        if (previusSelectedWeapon != selectetWeapon)
        {
            SelectetWeapon();
        }
        
    }
    void SelectetWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectetWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
          


        }
    }
}

