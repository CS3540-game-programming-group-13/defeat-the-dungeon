using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; set; }

    [SerializeField]
    private Text gameText;
    [SerializeField]
    private AudioClip gameOverSFX;
    //[SerializeField]
    //private AudioClip yaySFX;
    [SerializeField]
    private string nextLevel;
    private float countDown;
    private bool isGameOver = false;

    private void Start()
    {
        isGameOver = false;
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LevelLost(){
        isGameOver = true;
        gameText.text = "You Died";
        gameText.gameObject.SetActive(true);
        UIFade.instance.FadeOut(0.25f);
        Invoke("LoadSameLevel", 2);
    }
    
    public void LevelBeat(){
        isGameOver = true;
        //gameText.text = "YOU WIN!";
        //gameText.gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(nextLevel))
        {
            UIFade.instance.FadeOut(0.25f);
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

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
