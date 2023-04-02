using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public GameObject player;
    public float showDistance = float.MaxValue;
    public GameObject potionPrefab;

    public AudioClip chestOpenSFX;

    private bool isOpen = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance <= showDistance && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            // add code to open the chest here
            Debug.Log("Chest Opened");
            isOpen = true;
            anim.SetBool("chestOpened", isOpen);

            AudioSource.PlayClipAtPoint(chestOpenSFX, Camera.main.transform.position);

            for(int i = 0; i < 3; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 2f), 0f);
                GameObject potion = Instantiate(potionPrefab, transform.position + offset, Quaternion.identity);
            }
        }
    }
}
