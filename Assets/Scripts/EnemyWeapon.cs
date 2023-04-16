using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int damage = 20;
    private bool shouldApplyDamage = true;
    private bool appliedDamage = false;

    void Update()
    {
        if (gameObject.activeInHierarchy && !appliedDamage)
        {
            shouldApplyDamage = true;
        }
    }

    private void OnEnable()
    {
        appliedDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && shouldApplyDamage && !ShieldItemBehavior.IsBlocking)
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            shouldApplyDamage = false;
            appliedDamage = true;
        }
    }
}
