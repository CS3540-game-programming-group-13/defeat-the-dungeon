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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            DestroyCrate();
        }
    }

    void DestroyCrate()
    {
        Instantiate(crateObject, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
