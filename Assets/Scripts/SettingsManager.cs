using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance { get { return _instance; } }

    public float startBoostAcceleration;
    public float endBoostAcceleration;//boost speed minus player acceleration
    public float boostDuration;

    public float startTimeCount;

    public float startPlayerSpeed;
    public Vector3 startPlayerPosition;

    public int obstacleTimeFine;
    public int timeBonus;
    public float timeMessageDuration;

    public float rocketDuration;
    public float timingLaserTime;

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

        DontDestroyOnLoad(this.gameObject);
    }
}
