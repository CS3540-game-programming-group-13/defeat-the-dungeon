using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestOpenBehavior : MonoBehaviour
{
    public static bool isChestOpened = false;
    public GameObject chestMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isChestOpened)
            {
                ChestCollected();
            }
            else
            {
                ChestOpened();
            }
        }
    }

    public void ChestOpened()
    {
        isChestOpened = true;
        Time.timeScale = 0f;
        chestMenu.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChestCollected()
    {
        isChestOpened = false;
        Time.timeScale = 1f;
        chestMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
