using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivate : MonoBehaviour
{
    public AudioClip trapSFX;
    PlayerStats health;

    private Animator anim;

    void Start()
    {
        health = FindObjectOfType<PlayerStats>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("steppedOn", true);
            AudioSource.PlayClipAtPoint(trapSFX, Camera.main.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("steppedOn", false);
        }
    }
}
