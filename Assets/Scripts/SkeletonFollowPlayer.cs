using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float jumpForce;
    public float followRange;
    public float stopRange; 
    public Rigidbody2D rb;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public bool isGrounded = true;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private bool isFacingRight;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        moveEnemy();
        animateEnemy();
    }
    private void moveEnemy()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < followRange && distance > stopRange)
        {
            Vector2 directionToTarget = (player.position - transform.position).normalized * speed;
            rb.linearVelocity = new Vector2(directionToTarget.x, rb.linearVelocity.y);
        }

        if (rb.linearVelocity.x > 0.1f)
        {
            isFacingRight = true;
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            isFacingRight = false;
        }
        
    }


    public void jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void animateEnemy()
    {
        // Debug.Log(Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        spriteRenderer.flipX = !isFacingRight;
    }

}
