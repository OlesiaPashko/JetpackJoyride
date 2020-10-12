using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.5f;
    public float upForce = 1;
    public Rigidbody2D rigidbody;
    public float acceleration = 0.5f;
    public int cointsCount = 0;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        speed += acceleration;
        Vector3 direction = Vector3.right * speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
        if (Input.GetButton("Jump")) 
        {
            rigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        }
    }

    private void OnMouseDown()
    {
        
    }
}
