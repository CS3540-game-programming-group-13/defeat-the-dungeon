using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemBehavior : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private BoxCollider sword;

    public AudioClip swingSFX;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(sword == null)
        {
            sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("playerSlash");
            AudioSource.PlayClipAtPoint(swingSFX, transform.position);
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
