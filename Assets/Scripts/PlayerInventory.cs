using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; set; }

    public AudioClip potionDrinkSFX;
    public static int potionCount = 0;
    [SerializeField]
    private Text potionCountText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
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

    public void AddPotion(int numPotions)
    {
        potionCount+= numPotions;
        UpdatePotionCounter();
    }

    void UsePotion()
    {
        PlayerStats.instance.Heal(20);
        AudioSource.PlayClipAtPoint(potionDrinkSFX, Camera.main.transform.position);
        potionCount--;
        UpdatePotionCounter();
    }

    void UpdatePotionCounter()
    {
        potionCountText.text = potionCount.ToString();
    }

    public int PotionCount { 
        get
        {
            return potionCount;
        } 
    }

    public void UpdateCounterTextComponent()
    {
        potionCountText = GameObject.FindGameObjectWithTag("PotionCounter").GetComponent<Text>();
        Debug.Log("Updating: " + potionCountText);
        UpdatePotionCounter();
    }
}
