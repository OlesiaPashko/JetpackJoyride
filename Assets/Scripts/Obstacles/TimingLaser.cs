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
        //Line stays thin for some time
        yield return new WaitForSeconds(SettingsManager.Instance.timingLaserTime);

        //Line becomes wide and harmful
        float bigLineWidth = 2f;
        volumetricLineBehavior.LineWidth = bigLineWidth;
        this.gameObject.AddComponent<Obstacle>();

        //Line destroys with delay
        yield return new WaitForSeconds(SettingsManager.Instance.timingLaserTime);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //Move towards camera
        Vector3 laserOffset = new Vector3(-5f, 0f, 0f);
        Vector3 position = cameraPivot.position + laserOffset;
        position.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, position, distance);
    }
}
