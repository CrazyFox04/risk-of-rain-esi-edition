using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public bool isGrounded = true;
    public bool isFacingRight = true;

    private bool isFalling = false;
    private bool isDashing = false;
    private bool isJetPacking = false;
    private bool isPerformingAnimation = false;
    private bool isHurt = false;
    public bool isBussy = false;
    
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    public float jetPackForce;
    public float hurtTime;
    public float landingTime;
    

    public float dashTime;
    public float attack1Time;
    public float attack2Time;
    public float attack3Time;
    
    public float jetPackTime;

    public float dashCooldown;
    public float attack1Cooldown;
    public float attack2Cooldown;
    public float attack3Cooldown;
    public float jetPackCooldown;
    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public const string IDLE = "PlayerIdle";
    public const string ATTACK1 = "PlayerAttack1";
    public const string ATTACK2 = "PlayerAttack2";
    public const string ATTACK3 = "PlayerAttack3";
    public const string ATTACK4 = "PlayerAttack4";
    public const string ATTACK5 = "PlayerAttack5";
    public const string RUN = "PlayerRun";
    public const string FALLING = "PlayerFalling";
    public const string LANDING = "PlayerLanding";
    public const string DASH = "PlayerDash";
    public const string JETPACK = "PlayerJetPack";
    public const string HURT = "PlayerHurt";

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        if (isBussy) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }
        if (Input.GetButtonDown("Dash"))
        {
            dash();
        }

        if (Input.GetButtonDown("JetPack"))
        {
            jetPack();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            attack1();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            attack2();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            attack3();
        }

        movePlayer();
        updateBaseAnimation();
        FlipPlayerToMouse();
    }

    void movePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    void jump()
    {
        
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void dash()
    {
        StartCoroutine(BlockActionsCoroutine(dashTime));
        StartCoroutine(performAnimation(DASH, dashTime));
        if (isFacingRight)
            rb.AddForce(new Vector2(dashForce, 0f));
        else
            rb.AddForce(new Vector2(-dashForce, 0f));
    }

    void jetPack()
    {
        StartCoroutine(performAnimation(JETPACK, jetPackTime));
        StartCoroutine(BlockActionsCoroutine(jetPackTime));
        StartCoroutine(performJetPack());
    }
    IEnumerator performJetPack()
    {
        isJetPacking = true;
        rb.AddForce(new Vector2(0f, jetPackForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jetPackTime);
        isJetPacking = false;
        yield return new WaitForSeconds(jetPackCooldown);
    }

    void attack1()
    {
        StartCoroutine(BlockActionsCoroutine(attack1Time));
        StartCoroutine(performAnimation(ATTACK1, attack1Time));
    }

    void attack2()
    {
        StartCoroutine(BlockActionsCoroutine(attack2Time));
        StartCoroutine(performAnimation(ATTACK2, attack2Time));
    }

    void attack3()
    {
        StartCoroutine(BlockActionsCoroutine(attack3Time));
        StartCoroutine(performAnimation(ATTACK3, attack3Time));
    }

    public void addMaxHealth(int health)
    {
        maxHealth += health;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void addHealth(int health)
    {
        currentHealth += health;
        healthBar.setHealth(currentHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.setHealth(currentHealth);
        }
    }

    public void takeDamage(int damage)
    {
        StartCoroutine(performAnimation(HURT, hurtTime));
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            healthBar.setHealth(currentHealth);
        }
    }

    void updateBaseAnimation()
    {
        if (isPerformingAnimation) return;
        
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (!isGrounded)
        {
            changeAnimationState(FALLING);
        } else if (horizontalMovement != 0)
        {
            changeAnimationState(RUN);
        }
        else
        {
            changeAnimationState(IDLE);
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(performAnimation(LANDING, landingTime));
        }
    }
    
    private IEnumerator performAnimation(string animation, float attackTime)
    {
        isPerformingAnimation = true;
        changeAnimationState(animation);
        yield return new WaitForSeconds(attackTime);
        isPerformingAnimation = false;
    }
    
    private IEnumerator BlockActionsCoroutine(float time)
    {
        isBussy = true;
        yield return new WaitForSeconds(time);
        isBussy = false;
    }

    public void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }

    void SetPlayerOrientation(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
        isFacingRight = faceRight;
    }

    void FlipPlayerToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x)
        {
            SetPlayerOrientation(true);
        }
        else
        {
            SetPlayerOrientation(false);
        }
    }
}