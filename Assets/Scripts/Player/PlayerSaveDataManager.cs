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
        public float MouseSensitivity;
    }

    public static PlayerSaveDataManager instance { get; set; }
    public PlayerSaveData playerData;
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
            playerData.RawPlayTimeSeconds = 0;
            playerData.MouseSensitivity = 100;
            SavePlayerData();
        }
        StartCoroutine(RecordTimeRoutine());
    }

    public void SetMouseSensitivity(float newSensitivity)
    {
        playerData.MouseSensitivity = newSensitivity;
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
            playerData.RawPlayTimeSeconds += 1;
        }
    }

}
