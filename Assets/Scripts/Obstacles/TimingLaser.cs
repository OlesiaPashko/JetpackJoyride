using System.Collections;
using UnityEngine;
using VolumetricLines;

public class TimingLaser : MonoBehaviour
{
    VolumetricLineBehavior volumetricLineBehavior;
    public Player player;
    public float distance;
    void Start()
    {
        volumetricLineBehavior = GetComponent<VolumetricLineBehavior>();
        player = FindObjectOfType<Player>();
        StartCoroutine(BehaviorCoroutine());
    }

    IEnumerator BehaviorCoroutine()
    {
        yield return new WaitForSeconds(2);

        volumetricLineBehavior.LineWidth = 2f;
        this.gameObject.AddComponent<Obstacle>();

        yield return new WaitForSeconds(2);
        volumetricLineBehavior.LineWidth = 0.1f;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        var playerPos = player.gameObject.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x+2.5f, transform.position.y, transform.position.z), distance);
    }
}
