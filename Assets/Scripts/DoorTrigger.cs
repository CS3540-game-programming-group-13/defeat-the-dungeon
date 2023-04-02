using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public AudioClip doorCreakSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && EnemyAnimBehavior.enemyCount == 0)
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
