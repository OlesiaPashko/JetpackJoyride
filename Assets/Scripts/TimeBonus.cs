using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TimeManager timeManager = TimeManager.Instance;
            timeManager.AddTime(SettingsManager.Instance.timeBonus);
            Destroy(gameObject);
        }
    }
}
