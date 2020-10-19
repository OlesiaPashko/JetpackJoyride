using System.Collections;
using UnityEngine;
using VolumetricLines;

public class TimingLaser : MonoBehaviour
{
    VolumetricLineBehavior volumetricLineBehavior;
    public Transform cameraPivot;
    public float distance;
    void Start()
    {
        volumetricLineBehavior = GetComponent<VolumetricLineBehavior>();
        cameraPivot = Camera.main.transform;
        StartCoroutine(BehaviorCoroutine());
    }

    IEnumerator BehaviorCoroutine()
    {
        yield return new WaitForSeconds(SettingsManager.Instance.timingLaserTime);

        float bigLineWidth = 2f;
        volumetricLineBehavior.LineWidth = bigLineWidth;
        this.gameObject.AddComponent<Obstacle>();

        yield return new WaitForSeconds(SettingsManager.Instance.timingLaserTime);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 laserOffset = new Vector3(-5f, 0f, 0f);
        Vector3 position = cameraPivot.position + laserOffset;
        position.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, position, distance);
    }
}
