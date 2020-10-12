using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = Vector3.right * speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }

}
