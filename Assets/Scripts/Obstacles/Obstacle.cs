using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TimeManager timeManager = TimeManager.Instance;
            timeManager.ReduceTime(10);
            Debug.Log("OBSTACLE " + timeManager.TimeCount);
        }
    }
}
