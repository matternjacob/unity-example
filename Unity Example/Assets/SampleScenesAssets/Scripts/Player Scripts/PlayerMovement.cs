using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    float groundCheckRadius = 0.2f;
    public float speed = 5f;
    private bool isFacingRight = true;     
    private float horizontal;
    public float jumpingPower;
    public bool isGrounded;
    public bool isJumping;
    public Animator animator;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] CharacterController controller;
    [SerializeField] Vector2 moveDirection;
    [SerializeField] Transform groundCheckCollider;


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
        }
       

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            isJumping = true;   
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }


        //Checks if player is in the air
        if (!isGrounded)
        {
            isJumping = false;
        }

        //set yVelocity in animator
        animator.SetFloat("yVelocity", rb.velocity.y);




        Flip();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

    }
    void groundCheck()
    {

        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;

        animator.SetBool("isJumping", !isGrounded);
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

