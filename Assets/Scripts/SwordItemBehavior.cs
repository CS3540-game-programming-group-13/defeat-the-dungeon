using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemBehavior : MonoBehaviour
{
    private Animator animator;
    private BoxCollider sword;

    void Start()
    {
        animator = GetComponent<Animator>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        Debug.Log("SWORD: " + sword);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("playerSlash");
        }
    }

    public void DisableSwordCollider()
    {
        sword.enabled = false;
    }

    public void EnableSwordCollider()
    {
        sword.enabled = true;
    }
}
