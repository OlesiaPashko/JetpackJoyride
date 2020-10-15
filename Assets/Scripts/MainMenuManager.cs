using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text maxScore;
    public Text coisCount;

    private void Start()
    {
        maxScore.text = DataHolder.GetMaxScore().ToString();
        coisCount.text = DataHolder.GetCoinsCount().ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
