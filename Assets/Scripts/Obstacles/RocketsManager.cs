using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsManager : MonoBehaviour
{
    public Rocket RocketPrefab;
    void Start()
    {
        SectionManager.Instance.OnSectionSpawned += SpawnRocket;
    }

    private void SpawnRocket()
    {
        Instantiate(RocketPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
}
