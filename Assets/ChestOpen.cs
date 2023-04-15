using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestOpen : MonoBehaviour
{
    public GameObject player;
    public float showDistance = float.MaxValue;

    public int potionAmount;

    public AudioClip chestOpenSFX;

    private bool isOpen = false;
    private Animator anim;
    public GameObject chestMenu;

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
            OpenChestMenu();
        }
    }
    public void OpenChestMenu()
    {
        Time.timeScale = 0f;
        chestMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseChestMenu()
    {
        Time.timeScale = 1f;
        chestMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Collected()
    {
        PlayerInventory.instance.potionCount = potionAmount - 1;
        PlayerInventory.instance.AddPotion();
        CloseChestMenu();
    }
}
