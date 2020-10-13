using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning: MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 150 * Time.deltaTime);
    }
}
