using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; set; }

    [SerializeField]
    private float levelDuration = 100.0f;
    [SerializeField]
    private Text gameText;
    [SerializeField]
    private AudioClip gameOverSFX;
    [SerializeField]
    private AudioClip yaySFX;
    [SerializeField]
    private string nextLevel;

    private float enemyCount;
    private float countDown;
    private bool isGameOver = false;

    private void Start()
    {
        isGameOver = false;
        countDown = levelDuration;
        enemyCount = 1; // TODO: get number of game objects with tag 'enemy'
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(!isGameOver) {
            if(countDown > 0) {
                countDown -= Time.deltaTime;
            }
            else {
                countDown = 0.00f;
                LevelLost();
            }
            if(enemyCount <= 0) {
                LevelBeat();
            }
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
        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
