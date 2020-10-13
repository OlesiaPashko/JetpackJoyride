using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using VolumetricLines;

public class TimingLaser : MonoBehaviour
{
    VolumetricLineBehavior volumetricLineBehavior;
    // Start is called before the first frame update
    void Start()
    {
        volumetricLineBehavior = GetComponent<VolumetricLineBehavior>();
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);

        volumetricLineBehavior.LineWidth = 1f;
        this.gameObject.AddComponent<Obstacle>();

        yield return new WaitForSeconds(5);
        volumetricLineBehavior.LineWidth = 0.1f;
        Destroy(gameObject);
    }
}
