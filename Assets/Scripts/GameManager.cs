using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int coinsCount = 0;
    public bool isPause = false;
    private float score = 0f;
   
    public Player player;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            GameUIManager.Instance.SetScore(Score);
        }
    }
    public int CoinsCount
    { 
        get { return coinsCount; }
        set
        {
            coinsCount = value;
            GameUIManager.Instance.SetCoinsCount(CoinsCount);
        }
    } 
    

    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        isPause = false;
        Time.timeScale = 1;
        GameUIManager.Instance.GameOn();

        UseBoosts();
    }

    private void UseBoosts()
    {
        int boostsAmount = DataHolder.GetAmount(Bonus.Boost);
        if (boostsAmount > 0)
        {
            DataHolder.TryDecrementAmount(Bonus.Boost);
            StartCoroutine(Boost());
        }
    }

    private IEnumerator Boost()
    {
        //Boost player
        player.gameObject.tag = "Disabled";
        player.speed += SettingsManager.Instance.startBoostAcceleration;
        yield return new WaitForSeconds(SettingsManager.Instance.boostDuration);

        //Change speed mode to normal
        player.speed -= SettingsManager.Instance.endBoostAcceleration;
        player.gameObject.tag = "Player";

        //Use boosts if there are some more
        if (DataHolder.TryDecrementAmount(Bonus.Boost))
        {
            StartCoroutine(Boost());
        }

        //Set time to start time amount
        TimeManager.Instance.TimeCount = SettingsManager.Instance.startTimeCount;
    }

    public void EndGame()
    {
        //Set pause
        PauseGame();
        GameUIManager.Instance.GameOver(CoinsCount, Score);

        //save coins ad score
        DataHolder.AddCoinsCount(CoinsCount);
        if (score > DataHolder.GetMaxScore())
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
        GameUIManager.Instance.GameOn();
        isPause = false;
        Time.timeScale = 1;
    }

    public void UseExtraLive()
    {
        if (DataHolder.TryDecrementAmount(Bonus.Life))
        {
            TimeManager.Instance.TimeCount = SettingsManager.Instance.startTimeCount;
            ResumeGame();
        }
        else
        {
            Debug.Log("You don`t have extra lives. Do to shop to buy some");
        }
    }

    public void RestartGame()
    {
        //Set coins and score to zero
        DataHolder.AddCoinsCount(CoinsCount);
        coinsCount = 0;
        score = 0f;

        Start();

        //Set initial values
        TimeManager.Instance.TimeCount = SettingsManager.Instance.startTimeCount;
        player.speed = SettingsManager.Instance.startPlayerSpeed;
        SectionManager.Instance.Restart();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
