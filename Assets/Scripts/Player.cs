using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4.5f;
    public float upForce = 1;
    public Rigidbody2D rigidbody;
    public float acceleration = 0.001f;
    public GroundDetection groundDetection;
    public Animator animator;
    public AnimatorOverrideController knightAnimator;
    void Start()
    {
        Skins activeSkin = DataHolder.GetActiveSkin();
        if(activeSkin == Skins.Knight)
            animator.runtimeAnimatorController = knightAnimator;
    }
    void FixedUpdate()
    {
        animator.SetBool("isGrounded", groundDetection.isGrounded);

        MoveForward();

        if (Input.GetButton("Jump"))
        {
            rigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        }

        GameManager.Instance.Score += speed * Time.deltaTime;
    }

    private void MoveForward()
    {
        speed += acceleration;
        Vector3 direction = Vector3.right * speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }
}
