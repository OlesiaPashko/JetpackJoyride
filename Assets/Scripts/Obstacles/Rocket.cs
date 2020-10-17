using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 0.25f;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Player player;
    public bool isWarning = true;
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player.gameObject.CompareTag("Disabled"))//there are no rockets in boost mode
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(RocketBehavior());
    }

    public IEnumerator RocketBehavior()
    {
            Vector3 direction = Vector3.left * speed;
            direction.y = rigidbody.velocity.y;
            rigidbody.velocity = direction;
            yield return new WaitForSeconds(3f);

            isWarning = false;
            animator.SetTrigger("StartFly");
            yield return new WaitForSeconds(3f);

            Destroy(gameObject);
    }

    private void Update()
    {
        if (isWarning)
        {
            var playerPos = player.gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x + 15f, playerPos.y, transform.position.z), 10);
        }
    }
}
