using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public Text coins;
    public Text score;
    public Text lives;

    public void Init(int coinsCount, float scoreCount)
    {
        coins.text = coinsCount.ToString();
        lives.text = DataHolder.GetAmount(Bonus.Life).ToString();
        score.text = ((int)scoreCount).ToString();
    } 
}
