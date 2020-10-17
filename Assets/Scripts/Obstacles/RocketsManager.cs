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

    private void SpawnRocket()
    {
        //To DO debug in better way
        if (!done)//without this if 4 rockets instantiates.
        {
            Instantiate(RocketPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            done = true;
        }
    }
}
