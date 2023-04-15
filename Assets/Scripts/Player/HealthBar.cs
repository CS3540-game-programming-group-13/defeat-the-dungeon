using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Color color;
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private TextMeshProUGUI healthText;
    private PlayerStats playerStats;
    [SerializeField]
    private float speed = 1;
    
    void Start()
    {
        foregroundImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        foregroundImage.color = color;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }


    void Update()
    {
        if (foregroundImage)
        {
            foregroundImage.fillAmount =  Mathf.Lerp(foregroundImage.fillAmount , (float)playerStats.CurrentHealth / (float)playerStats.startingHealth, speed * Time.deltaTime);
            healthText.text = string.Format("{0}/{1}", playerStats.CurrentHealth, playerStats.startingHealth);
        }
    }
}
