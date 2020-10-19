using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TimeManager.Instance.AddTime(SettingsManager.Instance.timeBonus);
            Destroy(gameObject);
        }
    }
}
