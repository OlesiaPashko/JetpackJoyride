using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public static TimeManager Instance { get { return _instance; } }

    private float time = SettingsManager.Instance.startTimeCount;
    
    [SerializeField] public float TimeCount 
    { 
        get { return time; } 
        set 
        { 
            time = value;
            GameUIManager.Instance.SetTimeCount(value);
        } 
    }

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


    private void Update()
    {
        if (!GameManager.Instance.isPause)
        {
            TimeCount -= Time.deltaTime;
            if (this.time <= 0)
            {
                time = 0;
                GameManager.Instance.EndGame();
            }
        }
    }

    public void AddTime(int time)
    {
        TimeCount += time;
        GameUIManager.Instance.ShowTimeBonus();
    }

    public void ReduceTime(int time)
    {
        TimeCount -= time;
        GameUIManager.Instance.ShowTimeFine();
        if (this.time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }
}
