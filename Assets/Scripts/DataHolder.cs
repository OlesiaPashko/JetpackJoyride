using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    static DataHolder()
    {
        SetBought(Skins.Default);
        SetActiveSkin(Skins.Default);//this couses bug but I don`t know how make it better
    }

    public static Skins GetActiveSkin()
    {
        return (Skins)PlayerPrefs.GetInt("ActiveSkin");
    }

    public static void SetActiveSkin(Skins skin)
    {
        PlayerPrefs.SetInt("ActiveSkin", (int)skin);
    }

    public static bool IsBought(Skins skin)
    {
        return PlayerPrefs.GetInt(skin.ToString(), 0) > 0;
    }

    public static void SetBought(Skins skin)
    {
        PlayerPrefs.SetInt(skin.ToString(), 1);
    }

    public static void SetUnbought(Skins skin)//for testing
    {
        PlayerPrefs.SetInt(skin.ToString(), 0);
    }
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
    public static int GetAmount(Bonus item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }

    public static void IncrementAmount(Bonus item)
    {
        int amount = GetAmount(item);
        PlayerPrefs.SetInt(item.ToString(), ++amount);
        PlayerPrefs.Save();
    }

    public static bool TryDecrementAmount(Bonus item)
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
