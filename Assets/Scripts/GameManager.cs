using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int coinsCount = 0;
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
    }
}
