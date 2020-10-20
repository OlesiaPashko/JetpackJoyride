using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsManager : MonoBehaviour
{
    public Rocket rocketPrefab;
    public Player player;
    void Start()
    {
        StartCoroutine(SpawnRocketCoroutine());
    }

    private IEnumerator SpawnRocketCoroutine()
    {
        //Set probability of rocket spawn to 1/5
        int randomNumber = Random.Range(1, 6);
        bool shoultRocketSpawns = (randomNumber % 5) == 0;

        if (shoultRocketSpawns)
        {
            //Spawn rocket
            Rocket rocket = Instantiate(rocketPrefab, gameObject.transform.position, Quaternion.identity);
            rocket.SetPlayer(player.transform);
        }
        
        //Start it once again with delay
        yield return new WaitForSeconds(SettingsManager.Instance.rocketPause);
        StartCoroutine(SpawnRocketCoroutine());
    }
}
