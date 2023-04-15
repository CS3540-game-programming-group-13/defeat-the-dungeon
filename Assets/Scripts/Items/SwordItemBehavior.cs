using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemBehavior : MonoBehaviour
{
    public static SwordItemBehavior instance { get; set; }
    private Animator animator;
    [SerializeField]
    private BoxCollider sword;

    public AudioClip swingSFX;

    void Start()
    {
        animator = GetComponent<Animator>();
        instance = this;
        if (sword == null)
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

    public void SetSword(GameObject newSword)
    {
        sword = newSword.GetComponent<BoxCollider>();
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
