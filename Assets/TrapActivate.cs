using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivate : MonoBehaviour
{
    public int trapDamage = 10;
    public AudioClip trapSFX;
    PlayerStats health;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<PlayerStats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trap Activated!");
            anim.SetBool("steppedOn", true);
            AudioSource.PlayClipAtPoint(trapSFX, Camera.main.transform.position);
            health.TakeDamage(trapDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //isActive = false;
            anim.SetBool("steppedOn", false);
        }
    }
}
