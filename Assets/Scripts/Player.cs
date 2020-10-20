using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = SettingsManager.Instance.startPlayerSpeed;
    public Rigidbody2D rigidbody;
    public GroundDetector groundDetector;
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
        //Set animation
        animator.SetBool("isGrounded", groundDetector.isGrounded);

        MoveForward();

        //Jump on jump input
        if (Input.GetButton("Jump"))
        {
            rigidbody.AddForce(Vector2.up * SettingsManager.Instance.playerUpForce, ForceMode2D.Impulse);
        }

        //Add score
        GameManager.Instance.Score += speed * Time.deltaTime;
    }

    private void MoveForward()
    {
        speed += SettingsManager.Instance.playerAcceleration;
        Vector3 direction = Vector3.right * speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }
}
