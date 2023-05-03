using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpCooldown; // new variable to set the time delay between jumps
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool canJump = true;
    private float jumpTimer = 0f; // new variable to track the jump cooldown

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // only allow jump if the player is grounded, canJump is true, and the jump cooldown has elapsed
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && canJump && jumpTimer <= 0f)
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
            jumpTimer = jumpCooldown; // start the jump cooldown timer
        }

        // reset canJump to true if the player lands on the ground
        if (isGrounded)
        {
            canJump = true;
        }

        // decrement the jump cooldown timer
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
