using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsManager : MonoBehaviour
{
    public Rocket RocketPrefab;
    public bool done = false;
    void Start()
    {
        SectionManager.Instance.SpawnObstacles += SpawnRocket;
    }

    private void SpawnRocket(float x)
    {
        if (!done)
        {
            Instantiate(RocketPrefab, new Vector3(x, 0f, 0f), Quaternion.identity);
            Debug.Log("Rocket was spawned");
            done = true;
        }
    }
}
