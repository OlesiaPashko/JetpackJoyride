using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsManager : MonoBehaviour
{
    public Rocket RocketPrefab;
    public Player player;
    void Start()
    {
        StartCoroutine(SpawnRocketCoroutine());
    }

    private IEnumerator SpawnRocketCoroutine()
    {
        //Set probability of rocket spawn to 1/5
        int rndNumber = Random.Range(1, 6);
        bool shoultRocketSpawns = (rndNumber % 5) == 0;

        if (shoultRocketSpawns)
        {
            //Spawn rocket
            Rocket rocket = Instantiate(RocketPrefab, gameObject.transform.position, Quaternion.identity);
            rocket.SetPlayer(player.transform);
        }
        
        //Start it once again with delay
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnRocketCoroutine());
    }
}
