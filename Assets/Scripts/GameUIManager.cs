using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager _instance;
    public Canvas gameOverCanvas;
    public Text coinsCount;
    public Text score;
    public Text time;
    public Text timeFine;
    public Text timeBonus;
    public static GameUIManager Instance { get { return _instance; } }

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

    public void SetScore(float score)
    {
        this.score.text = ((int)score).ToString();
    }
    
    public void SetCoinsCount(int coins)
    {
        coinsCount.text = coins.ToString();
    }

    public void SetTimeCount(float time)
    {
        TimeSpan span = TimeSpan.FromSeconds((double)(new decimal(time)));
        this.time.text = span.ToString(@"mm\:ss");
    }

    public void ShowTimeFine()
    {
        StartCoroutine(TimeFineCoroutine());
    }

    IEnumerator TimeFineCoroutine()
    {
        timeFine.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        timeFine.gameObject.SetActive(false);
    }

    public void ShowTimeBonus()
    {
        StartCoroutine(TimeBonusCoroutine());
    }

    IEnumerator TimeBonusCoroutine()
    {
        timeBonus.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        timeBonus.gameObject.SetActive(false);
    }
    public void GameOver(int coins, float score)
    {
        gameOverCanvas.gameObject.SetActive(true);
        Text[] texts = gameOverCanvas.GetComponentsInChildren<Text>();
        texts.First(x => x.name == "CoinsCount").text = coins.ToString();
        texts.First(x => x.name == "LivesCount").text = DataHolder.GetAmount(Bonus.Life).ToString();
        texts.First(x => x.name == "Score").text = ((int)score).ToString();
    }

    public void GameOn()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }
}
