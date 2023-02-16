using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashContact : MonoBehaviour
{
    public GameObject crateObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // in-contact with Sword go to destroy crate
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            DestroyCrate();
        }
    }

    // for multiple crates, destroy when in contact with Sword
    void DestroyCrate()
    {
        Instantiate(crateObject, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
