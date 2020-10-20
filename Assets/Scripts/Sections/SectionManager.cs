using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    public Section[] SectionPrefabs;
    public Section[] FirstPrefabs;
    private List<Section> spawnedSections = new List<Section>();
    private static SectionManager instance;
    public Player player;
    public static SectionManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        foreach (var prefab in FirstPrefabs)
            spawnedSections.Add(Instantiate(prefab));
    }
    public void SpawnSection()
    {
        //Create new section
        Section newSection = Instantiate(SectionPrefabs[UnityEngine.Random.Range(0, SectionPrefabs.Length)]);

        //Add new section to end of spawned sections line
        newSection.transform.position = GetPositionToSpawn();
        spawnedSections.Add(newSection);

        //Remove first section
        Destroy(spawnedSections[0].gameObject);
        spawnedSections.RemoveAt(0);
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
        //Destroy all spawned sections
        foreach (var spawnedSection in spawnedSections)
        {
            Destroy(spawnedSection.gameObject);
        }
        
        //Restart spawning
        spawnedSections = new List<Section>();
        Start();

        //Set player position
        player.gameObject.transform.position = spawnedSections[1].transform.position + SettingsManager.Instance.startPlayerPosition;
    }
}
