using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 100.0f;
    public Text timerText;
    public Text gameText;
    public AudioClip gameOverSFX;
    public AudioClip yaySFX;
    public string nextLevel;
    public string sameLevel;
    public float pickupCount;
    public float displayCount;
    

    public static bool isGameOver = false;

    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        countDown = levelDuration;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver){
        if(countDown > 0){
        countDown -= Time.deltaTime;
        }
        else{
            countDown = 0.00f;
            LevelLost();
        }
        if(pickupCount <= 0){
            LevelBeat();
        }
        SetTimerText();
        }

    }
    public void LevelLost(){
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);
        Invoke("LoadSameLevel", 2);

    }
    
    private void LevelBeat(){
        isGameOver = true;
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);
        Invoke("LoadLevelNext", 2);

    }
    void LoadLevelNext()
    {
        SceneManager.LoadScene(nextLevel);
    }
    void LoadSameLevel()
    {
        SceneManager.LoadScene(sameLevel);
    }
    void SetTimerText()
    {
        timerText.text = countDown.ToString("0.00");
    }
}
