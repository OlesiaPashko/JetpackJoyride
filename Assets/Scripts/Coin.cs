using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.CoinsCount++;
            Debug.Log(gameManager.CoinsCount);
            Destroy(gameObject);

        }
    }
}
