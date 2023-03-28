using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public AudioClip doorCreakSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(doorCreakSFX, transform.position);
            LevelManager instance = LevelManager.instance;
            if (instance)
            {
                LevelManager.instance.LevelBeat();
            }
        }
    }
}
