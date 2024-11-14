using UnityEngine;

public class MouvementPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    public Rigidbody2D rb;
    private Vector3  velocity = Vector2.zero;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        

        movePlayer(horizontalMovement);
    }

    void movePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}