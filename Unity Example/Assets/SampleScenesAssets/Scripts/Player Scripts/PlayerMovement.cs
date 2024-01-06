using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool isFacingRight = true;     
    private float horizontal;
    public float jumpingPower;
    private bool isGrounded;
    bool isJumping;
    public Animator animator;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] CharacterController controller;
    [SerializeField] Vector2 moveDirection;


    void Start() //Rigid body pull
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }


    void Update() //Horizontal and Jumping Functions
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Jumping Code
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpingPower;
            isJumping = true;
        }

        //Checks if player is in the air
        if (!isGrounded)
        {
            isJumping = true;
        }

        Flip();

    }
            private void OnCollisionEnter2D(Collision2D collision)
            {
                isGrounded = true;
            }

            private void OnCollisionExit2D(Collision2D collision)
            {
                isGrounded = false;
            }


     void FixedUpdate() //if player is not grounded, close the function 
    { 
        if (!isGrounded)
        {
              return;
        }
    }

    private void Flip() //player flip when facing in direction of movement
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
           isFacingRight = !isFacingRight;
           Vector3 localScale = transform.localScale;
           localScale.x *= -1f;
           transform.localScale = localScale;
        }

        
    }

   
}

