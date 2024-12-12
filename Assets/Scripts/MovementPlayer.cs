using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
	public float moveSpeed;

	public float jumpForce;
	private bool isGrounded = true;
    private bool isJumping = false;

    public bool isDashing;
    public float dashForce;
    public float dashDuration;
    private float dashTime;

    public bool isFacingRight = true;
	public Rigidbody2D rb;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	public Animator animator;
	public SpriteRenderer spriteRenderer;

	void Update()
	{
		// Check if the player presses the jump button and is grounded.
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			isJumping = true;
		}

		// Check if the player presses the dash button and is not already dashing.
		if (Input.GetButtonDown("Dash") && !isDashing)
		{
			jump();
			startDash();
		}
		// Update the player's animation.
		animatePlayer();
	}

	void FixedUpdate()
	{
		// Check if the player is grounded using two ground check points.
		isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

		// If the player is dashing, perform the dash and return.
		if (isDashing)
		{
			performDash();
			return;
		}

		// Read the horizontal input from the user and move the player.
		float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		movePlayer(horizontalMovement);
	}

    void movePlayer(float horizontalMovement)
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y); // Déplacement sur X

        // If the player is jumping, start the jump and reset the jumping state.
        if (isJumping)
        {
            jump();
            isJumping = false;
        }

        // Update the player's facing direction based on horizontal velocity.
        if (rb.linearVelocity.x > 0.1f)
        {
            isFacingRight = true;
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            isFacingRight = false;
        }
    }

	void jump()
	{
		// Add a vertical force to the player to make them jump.
		rb.AddForce(new Vector2(0f, jumpForce));
	}

    public void weakJump()
    {
        // Add a vertical force to the player to make them jump.
        rb.AddForce(new Vector2(0f, jumpForce/2));
    }

    void startDash()
	{
		// Start the dash by setting the dashing state and dash time.
		isDashing = true;
		dashTime = dashDuration;
		if (isFacingRight)
            rb.AddForce(new Vector2(dashForce, 0f));
        else
            rb.AddForce(new Vector2(-dashForce, 0f));
    }

	void performDash()
	{
		// Decrease the dash time.
		dashTime -= Time.fixedDeltaTime;

		// If the dash time is over, stop dashing.
		if (dashTime <= 0)
		{
			isDashing = false;
		}
	}

	void animatePlayer()
	{
		// Update the animator with the player's speed.
		animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
		animator.SetBool("IsGrounded", isGrounded);
		animator.SetBool("IsDashing", isDashing);
		spriteRenderer.flipX = !isFacingRight;
	}
}
