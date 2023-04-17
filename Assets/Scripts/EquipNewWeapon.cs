using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipNewWeapon : MonoBehaviour
{
    public int damage;
    public GameObject weapon;

    public void OnButtonClick()
    {
        PlayerStats.instance.WeaponDamage = damage;
        SwordItemBehavior.instance.SetSword(weapon);
    }
}
