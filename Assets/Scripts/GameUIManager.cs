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
    public Text coinsCountText;
    public Text scoreText;
    public Text timeText;

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
        scoreText.text = ((int)score).ToString();
    }
    
    public void SetCoinsCount(int coins)
    {
        coinsCountText.text = coins.ToString();
    }

    public void SetTimeCount(float time)
    {
        TimeSpan span = TimeSpan.FromSeconds((double)(new decimal(time)));
        timeText.text = span.ToString(@"mm\:ss");
    }
    public void GameOver(int coins, float score)
    {
        gameOverCanvas.gameObject.SetActive(true);
        Text[] texts = gameOverCanvas.GetComponentsInChildren<Text>();
        texts.First(x => x.name == "CoinsCount").text = coins.ToString();
        texts.First(x => x.name == "LivesCount").text = DataHolder.GetAmount(ShopItem.Life).ToString();
        texts.First(x => x.name == "Score").text = ((int)score).ToString();
    }

    public void GameOn()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }
}
