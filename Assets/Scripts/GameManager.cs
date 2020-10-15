using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int coinsCount = 0;
    public Canvas gameOverCanvas;
    public bool isPause = false;
    private float score = 0f;
    public Text coinsCountText;
    public Text scoreText;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = ((int)score).ToString();
        }
    }
    public int CoinsCount
    { 
        get { return coinsCount; }
        set
        {
            coinsCount = value;
            coinsCountText.text = coinsCount.ToString();
        }
    } 
    

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        isPause = false;
        Time.timeScale = 1;
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("END OF GAME");
        PauseGame();
        gameOverCanvas.gameObject.SetActive(true);
        gameOverCanvas.GetComponentsInChildren<Text>().First(x => x.name == "CoinsCount").text = CoinsCount.ToString();
        gameOverCanvas.GetComponentsInChildren<Text>().First(x => x.name == "LivesCount").text = DataHolder.GetAmount(ShopItem.Life).ToString();
        gameOverCanvas.GetComponentsInChildren<Text>().First(x => x.name == "Score").text = ((int)score).ToString();
        DataHolder.AddCoinsCount(CoinsCount);
        if (IsMaxScore())
            DataHolder.SetMaxScore((int)score);
    }

    public void PauseGame()
    {
        Debug.Log("GAME Is On Pause");
        isPause = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameOverCanvas.gameObject.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }

    public void UseExtraLive()
    {
        if (DataHolder.TryDecrementAmount(ShopItem.Life))
        {
            TimeManager.Instance.TimeCount = 30;
            ResumeGame();
        }
        else
        {
            Debug.Log("You don`t have extra lives. Do to shop to buy some");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarts game");
        DataHolder.AddCoinsCount(CoinsCount);
        coinsCount = 0;
        score = 0f;
        Start();
        TimeManager.Instance.TimeCount = 30f;
        SectionManager.Instance.Restart();
    }

    private bool IsMaxScore()
    {
        return (score > DataHolder.GetMaxScore());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
