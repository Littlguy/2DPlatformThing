using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    public int health = 100;
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
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        extraJumpsCounter = extraJumpsAmount;
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * movespeed, rb.linearVelocity.y);
        if (isGrounded) 
        {
            extraJumpsCounter = extraJumpsAmount;
        }

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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
             health -= 25;
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

           if (health <= 0)
           {
             Die();
           }
        }
    }
    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    
}