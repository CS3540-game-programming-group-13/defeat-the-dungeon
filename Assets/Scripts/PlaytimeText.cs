using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaytimeText : MonoBehaviour
{
    private Text text;
    private string startingText;

    void Start()
    {
        text = GetComponent<Text>();
        startingText = text.text;
    }

    private void Update()
    {
        TimeSpan duration = TimeSpan.FromSeconds(PlayerSaveDataManager.instance.playerData.RawPlayTimeSeconds);
        text.text = startingText + duration.ToString("g");
    }
}
