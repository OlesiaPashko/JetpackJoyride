using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 0.25f;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public bool isWarning;
    public Transform player;
    void Start()
    {
        isWarning = true;
        StartCoroutine(RocketBehavior());
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    public IEnumerator RocketBehavior()
    {
        //Showing warning
        yield return new WaitForSeconds(SettingsManager.Instance.rocketDuration);

        //Showing and moving rocket
        MoveForward();
        isWarning = false;
        animator.SetTrigger("StartFly");

        //Destroy with delay
        yield return new WaitForSeconds(SettingsManager.Instance.rocketDuration);
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        transform.position += new Vector3(3f, 0f, 0f);

        Vector3 direction = Vector3.left * speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }

    private void Update()
    {
        if (isWarning)
        {
            //Move warning towards player
            Vector3 offset = new Vector3(15f, 0f, 0f);
            Vector3 position = player.position + offset;
            position.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, position, 10);
        }
    }
}
