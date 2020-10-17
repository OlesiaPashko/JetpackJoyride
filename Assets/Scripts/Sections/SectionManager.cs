using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public Section[] SectionPrefabs;
    public Section[] FirstPrefabs;
    private List<Section> spawnedSections = new List<Section>();
    private static SectionManager _instance;
    public Player player;
    public event Action SpawnObstacles;
    public static SectionManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        foreach (var prefab in FirstPrefabs)
            spawnedSections.Add(Instantiate(prefab));
    }
    public void SpawnSection()
    {
        Section newSection = Instantiate(SectionPrefabs[UnityEngine.Random.Range(0, SectionPrefabs.Length)]);

        newSection.transform.position = GetPositionToSpawn();
        spawnedSections.Add(newSection);

        Destroy(spawnedSections[0].gameObject);
        spawnedSections.RemoveAt(0);

        SpawnObstacles?.Invoke();
    }

    private Vector3 GetPositionToSpawn()
    {
        Vector3 lastElementPosition = spawnedSections[spawnedSections.Count - 1].transform.position;
        float sectorWidth = lastElementPosition.x - spawnedSections[spawnedSections.Count - 2].transform.position.x;
        float x = lastElementPosition.x + sectorWidth;
        return new Vector3(x, lastElementPosition.y, lastElementPosition.z);
    }

    public void Restart()
    {
        foreach(var spawnedSection in spawnedSections)
        {
            Destroy(spawnedSection.gameObject);
        }
        spawnedSections = new List<Section>();
        Start();
        player.gameObject.transform.position = spawnedSections[1].transform.position + new Vector3(4f, 5f, 0f);
    }
}
