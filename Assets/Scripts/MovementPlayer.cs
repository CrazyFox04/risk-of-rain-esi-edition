using UnityEngine;

public class MouvementPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    public Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        movePlayer(horizontalMovement);

        animatePlayer(rb.velocity.x);
    }

    void movePlayer(float horizontalMovement)
    {
        Vector2 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void animatePlayer(float speed)
    {
        animator.SetFloat("Speed", Mathf.Abs(speed));
        if (speed > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (speed < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}