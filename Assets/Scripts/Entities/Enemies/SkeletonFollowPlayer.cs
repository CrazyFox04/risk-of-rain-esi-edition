using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float followRange;
    [SerializeField] private float stopRange; 
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private bool isFacingRight;

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
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        spriteRenderer.flipX = !isFacingRight;
    }

}
