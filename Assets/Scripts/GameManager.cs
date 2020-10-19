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
        player.gameObject.tag = "Disabled";
        player.speed += SettingsManager.Instance.startBoostAcceleration;
        yield return new WaitForSeconds(SettingsManager.Instance.boostTime);

        player.speed -= SettingsManager.Instance.endBoostAcceleration;
        player.gameObject.tag = "Player";
        if (DataHolder.TryDecrementAmount(Bonus.Boost))
        {
            StartCoroutine(Boost());
        }
        TimeManager.Instance.TimeCount = SettingsManager.Instance.startTimeCount;
    }

    public void EndGame()
    {
        PauseGame();

        GameUIManager.Instance.GameOver(CoinsCount, Score);

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
        DataHolder.AddCoinsCount(CoinsCount);
        coinsCount = 0;
        score = 0f;

        Start();

        TimeManager.Instance.TimeCount = SettingsManager.Instance.startTimeCount;
        player.speed = SettingsManager.Instance.startPlayerSpeed;
        SectionManager.Instance.Restart();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
