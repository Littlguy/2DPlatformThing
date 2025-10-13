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

    public int extraJumpsAmount = 1;
    private int extraJumpsCounter;

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        extraJumpsCounter = extraJumpsAmount;
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * movespeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumpsCounter>0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumpsCounter--;
            }
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
                animator.Play("PlayerWalking");
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("PlayerJump");
            }
            else
            {
                animator.Play("PlayerFall");
            }
        }
    }
} 