using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItemBehavior : MonoBehaviour
{
    private Animator anim;
    private static bool isBlocking = false;

    public static bool IsBlocking { 
        get {
            return isBlocking;
        } 
    }

    private void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        isBlocking = Input.GetButton("Fire2");
        anim.SetBool("isBlocking", isBlocking);   
    }
}
