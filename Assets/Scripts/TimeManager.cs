using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public static TimeManager Instance { get { return _instance; } }

    [SerializeField] public float time { get; private set; }

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

    private void Start()
    {
        time = 1f;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        Debug.Log("Time = " + Mathf.Round(time).ToString());
        if (this.time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    public void AddTime(int time)
    {
        this.time += time;
    }

    public void ReduceTime(int time)
    {
        this.time -= time;
        if (this.time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }
}
