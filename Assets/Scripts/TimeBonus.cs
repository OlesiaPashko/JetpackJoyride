using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TimeManager timeManager = TimeManager.Instance;
            timeManager.AddTime(30);
            Debug.Log(timeManager.time);
            Destroy(gameObject);
        }
    }
}
