using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveDataManager : MonoBehaviour
{
    [Serializable]
    public struct PlayerSaveData
    {
        public int RawPlayTimeSeconds;
        public int PlayTimeHour;
        public int PlayTimeMinute;
        public int PlayTimeSecond;
        public float MouseSensitivity;
    }

    public static PlayerSaveDataManager instance { get; set; }
    public PlayerSaveData playerData;
    private bool shouldRecordTime = false;
    private const string saveFilePath = "./Assets/Data/player_data.json";

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
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

        if (File.Exists(saveFilePath))
        {
            LoadPlayerDataFromFile();
        }
        else
        {
            playerData = new PlayerSaveData();
            playerData.PlayTimeHour = 0;
            playerData.PlayTimeMinute = 0;
            playerData.PlayTimeSecond = 0;
            playerData.MouseSensitivity = 100;
            SavePlayerData();
        }
        StartCoroutine(RecordTimeRoutine());
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, json);
    }
    private void LoadPlayerDataFromFile()
    {
        string fileContents = File.ReadAllText(saveFilePath);
        playerData = JsonUtility.FromJson<PlayerSaveData>(fileContents);
    }

    private void Awake()
    {
        shouldRecordTime = !SceneManager.GetActiveScene().name.ToLower().Contains("menu");
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        SavePlayerData();
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    public IEnumerator RecordTimeRoutine()
    {
        TimeSpan ts;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (shouldRecordTime)
            {
                playerData.RawPlayTimeSeconds += 1;
                ts = TimeSpan.FromSeconds(playerData.RawPlayTimeSeconds);
                playerData.PlayTimeHour = (int)ts.TotalHours;
                playerData.PlayTimeMinute = ts.Minutes;
                playerData.PlayTimeSecond = ts.Seconds;
            }
        }
    }

}
