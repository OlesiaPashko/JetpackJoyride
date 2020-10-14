using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int coinsCount = 0;
    public Canvas gameOverCanvas;
    public bool isPause = false;
    public int CoinsCount
    { 
        get { return coinsCount; }
        set
        {
            coinsCount = value;
            coinsCountText.text = coinsCount.ToString();
        }
    } 
    public Text coinsCountText;

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

    public void EndGame()
    {
        Debug.Log("END OF GAME");
        PauseGame();
        gameOverCanvas.gameObject.SetActive(true);
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
        TimeManager.Instance.TimeCount = 30;
        ResumeGame();
    }
}
