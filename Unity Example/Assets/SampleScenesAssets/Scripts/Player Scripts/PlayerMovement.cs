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
    public float speed = 5f;
    private bool isFacingRight = true;     
    private float horizontal;
    public float jumpingPower;
    public bool isGrounded;
    public bool isJumping;
    public Animator animator;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Vector2 moveDirection;
    [SerializeField] Transform groundCheckCollider;


    void Start() //Rigid body and animator pull
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update() //Horizontal movement
    {
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0);

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Jumping Code
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpingPower;
            animator.SetBool("isJumping", true);
        }


        //set yVelocity in animator
        animator.SetFloat("yVelocity", rb.velocity.y);
        Flip();

    }

    //check if Grounded
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetFloat("yVelocity", 0);
        }

    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
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

