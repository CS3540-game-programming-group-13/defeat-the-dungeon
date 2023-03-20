using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int damage = 20;
    private bool wasActive = false;
    private bool shouldApplyDamage = true;

    private void Update()
    {
        if(gameObject.activeInHierarchy && !wasActive)
        {
            shouldApplyDamage = true;
        }
        wasActive = gameObject.activeInHierarchy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && shouldApplyDamage)
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            shouldApplyDamage = false;
        }
    }
}
