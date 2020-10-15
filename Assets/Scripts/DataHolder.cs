using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MaxScore");
    }

    public static void SetMaxScore(int score)
    {
        if (GetMaxScore() < score)
        {
            PlayerPrefs.SetInt("MaxScore", score);
            PlayerPrefs.Save();
        }
    }

    public static int GetCoinsCount()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public static void AddCoinsCount(int coinsCount)
    {
        PlayerPrefs.SetInt("Coins", coinsCount + GetCoinsCount());
        PlayerPrefs.Save();
    }

    public static bool TrySubtractCoinsCount(int coinsCount)
    {
        if (GetCoinsCount() - coinsCount >= 0)
        {
            PlayerPrefs.SetInt("Coins", GetCoinsCount() - coinsCount);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
    public static int GetAmount(ShopItem item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }

    public static void IncrementAmount(ShopItem item)
    {
        int amount = GetAmount(item);
        PlayerPrefs.SetInt(item.ToString(), ++amount);
        PlayerPrefs.Save();
    }

    public static bool TryDecrementAmount(ShopItem item)
    {
        int amount = GetAmount(item);
        if(amount > 0)
        {
            PlayerPrefs.SetInt(item.ToString(), --amount);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

}
