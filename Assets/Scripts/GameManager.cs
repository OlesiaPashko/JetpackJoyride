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
    public Player player;
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

        UseBoosts();
    }

    private void UseBoosts()
    {
        int boostsAmount = DataHolder.GetAmount(ShopItem.Boost);
        if (boostsAmount > 0)
        {
            DataHolder.TryDecrementAmount(ShopItem.Boost);
            StartCoroutine(Boost());
        }
    }

    private IEnumerator Boost()
    {
        player.gameObject.tag = "Disabled";
        player.speed += 60f;
        yield return new WaitForSeconds(7.5f);

        player.speed -= 51.5f;//boost speed minus acceleration
        player.gameObject.tag = "Player";
        if (DataHolder.TryDecrementAmount(ShopItem.Boost))
        {
            StartCoroutine(Boost());
        }
        TimeManager.Instance.TimeCount = 30f;
    }

    public void EndGame()
    {
        PauseGame();

        gameOverCanvas.gameObject.SetActive(true);
        Text[] texts = gameOverCanvas.GetComponentsInChildren<Text>();
        texts.First(x => x.name == "CoinsCount").text = CoinsCount.ToString();
        texts.First(x => x.name == "LivesCount").text = DataHolder.GetAmount(ShopItem.Life).ToString();
        texts.First(x => x.name == "Score").text = ((int)score).ToString();

        DataHolder.AddCoinsCount(CoinsCount);
        if (IsMaxScore())
            DataHolder.SetMaxScore((int)score);
    }

    public void PauseGame()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
        }
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
        DataHolder.AddCoinsCount(CoinsCount);
        coinsCount = 0;
        score = 0f;

        Start();

        TimeManager.Instance.TimeCount = 30f;
        player.speed = 4.5f;
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
