using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public static TimeManager Instance { get { return _instance; } }

    private float time = 30f;
    public Text timeText;
    [SerializeField] public float TimeCount 
    { 
        get { return time; } 
        private set 
        { 
            time = value;
            TimeSpan span = TimeSpan.FromSeconds((double)(new decimal(time)));
            timeText.text = span.ToString(@"mm\:ss"); 
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
        TimeCount -= Time.deltaTime;
        if (this.time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    public void AddTime(int time)
    {
        TimeCount += time;
    }

    public void ReduceTime(int time)
    {
        TimeCount -= time;
        if (this.time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }
}
