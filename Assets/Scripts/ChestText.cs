using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestText : MonoBehaviour
{
    public GameObject player;
    public GameObject chest;
    public GameObject chestLid;
    public Text text;
    public float showDistance = 5.0f;
    public float openChestRotation = 270.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chestLid = GetComponentInChildren<Transform>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, chest.transform.position);
        //if(distance <= showDistance && Input.GetKeyDown(KeyCode.F))
        //{
        //    // add code to open the chest here
        //    Debug.Log("Chest Opened");
        //    chestLid.GetComponent<Animator>().SetTrigger("ChestLidOpen");

        //}

        // display or hide the text based on the distance
        if(distance <= showDistance)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
