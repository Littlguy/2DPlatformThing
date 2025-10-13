using UnityEngine;

public class player : MonoBehaviour
{
    public float movespeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
 
    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * movespeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded ==true  )
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        SetAnimator(moveInput);
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
    }

    private void SetAnimator(float moveInput) 
    {
        if (isGrounded == true)
        {
            if (moveInput == 0)
            {
                animator.Play("PlayerIdle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
    }
} 