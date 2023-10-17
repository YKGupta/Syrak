using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;

    public Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    public event Action playerMoved;

    public event Action playerJumped;

    private void Update()
    {
        if(!PlayerManager.instance.isPlayerAllowedToMove)
        {
            animator.SetFloat("speedPercent", 0);
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0 || z != 0)
        {
            if(playerMoved != null)
                playerMoved();
        }

        Vector3 direction = transform.forward * z + transform.right * x;
        controller.Move(direction * speed * Time.deltaTime);

        bool jumpKeyPressed = Input.GetButtonDown("Jump");

        if(jumpKeyPressed)
        {
            if(playerJumped != null)
                playerJumped();
        }

        if(jumpKeyPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        float speedPercent = (x != 0 || z != 0 ? speed : 0) / speed;
        animator.SetFloat("speedPercent", speedPercent);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);     // gt^2
    }
}
