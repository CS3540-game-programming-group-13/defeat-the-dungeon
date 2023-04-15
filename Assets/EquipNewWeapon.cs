using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipNewWeapon : MonoBehaviour
{
    public void OnButtonClick()
    {
        PlayerStats.instance.WeaponDamage = 50;
    }
}
