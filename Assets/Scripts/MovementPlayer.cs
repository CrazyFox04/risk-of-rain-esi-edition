using UnityEngine;

public class MouvementPlayer : MonoBehaviour
{
	public float moveSpeed;
	public float jumpForce;
	public bool isJumping;
	public bool isGrounded;
	public bool isDashing;
	public bool isFacingRight = true;
	public Rigidbody2D rb;
	private Vector2 velocity = Vector2.zero;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	public Animator animator;
	public SpriteRenderer spriteRenderer;
	public float dashSpeed; 
	public float dashDuration; 
	private float dashTime;

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
			startJump();
			StartDash();
		}
		// Update the player's animation.
		animatePlayer(rb.linearVelocity.x);
	}

	void FixedUpdate()
	{
		// Check if the player is grounded using two ground check points.
		isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

		// If the player is dashing, perform the dash and return.
		if (isDashing)
		{
			PerformDash();
			return;
		}

		// Read the horizontal input from the user and move the player.
		float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		movePlayer(horizontalMovement);
	}

	void movePlayer(float horizontalMovement)
	{
		// Calculate the target velocity based on horizontal movement and current vertical velocity.
		Vector2 targetVelocity = new Vector2(horizontalMovement, rb.linearVelocity.y);
		rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

		// If the player is jumping, start the jump and reset the jumping state.
		if (isJumping)
		{
			startJump();
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

	void startJump()
	{
		// Add a vertical force to the player to make them jump.
		rb.AddForce(new Vector2(0f, jumpForce));
	}

	void StartDash()
	{
		// Start the dash by setting the dashing state and dash time.
		isDashing = true;
		dashTime = dashDuration;
		if (isFacingRight)
			rb.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, rb.linearVelocity.y);
		else
			rb.linearVelocity = new Vector2(-transform.localScale.x * dashSpeed, rb.linearVelocity.y);
	}

	void PerformDash()
	{
		// Decrease the dash time.
		dashTime -= Time.fixedDeltaTime;

		// If the dash time is over, stop dashing.
		if (dashTime <= 0)
		{
			isDashing = false;
		}
	}

	void animatePlayer(float speed)
	{
		// Update the animator with the player's speed.
		animator.SetFloat("Speed", Mathf.Abs(speed));
		animator.SetBool("IsGrounded", isGrounded);
		animator.SetBool("IsDashing", isDashing);
		spriteRenderer.flipX = !isFacingRight;
	}
}
