using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance { get { return _instance; } }

    public float startBoostAcceleration = 60f;
    public float endBoostAcceleration = 51.5f;//boost speed minus player acceleration
    public float boostTime = 7.5f;

    public float startTimeCount = 30f;

    public float startPlayerSpeed = 4.5f;
    public Vector3 startPlayerPosition = new Vector3(4f, 5f, 0f);

    public int obstacleTimeFine = 10;
    public int timeBonus = 5;

    public float rocketTime = 3f;
    public float timingLaserTime = 2f;

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
