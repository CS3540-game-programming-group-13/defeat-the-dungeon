using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorText : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (EnemyAnimBehavior.enemyCount > 0)
        {
            text.text = EnemyAnimBehavior.enemyCount + " enemies remaining";
        }
        else
        {
            text.text = "Touch to proceed";
        }
    }
}
