using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; set; }

    public AudioClip potionDrinkSFX;
    private int potionCount = 0;
    [SerializeField]
    private Text potionCountText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && potionCount > 0)
        {
            UsePotion();
        }
    }

    public void AddPotion()
    {
        potionCount++;
        if (potionCount == 1)
        {
            potionCountText.gameObject.SetActive(true);
        }
        potionCountText.text = "Health Potions: " + potionCount.ToString("d");
    }

    void UsePotion()
    {
        PlayerStats.instance.Heal(20);
        AudioSource.PlayClipAtPoint(potionDrinkSFX, Camera.main.transform.position);
        potionCount--;
        if (potionCount == 0)
        {
            potionCountText.gameObject.SetActive(false);
        }
        potionCountText.text = potionCount.ToString("d");
    }

    public int PotionCount { 
        get
        {
            return potionCount;
        } 
    }
}
