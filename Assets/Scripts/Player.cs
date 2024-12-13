using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    
    private bool isGrounded = true;
    public bool isFacingRight = true;
    
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;

    private bool blockAction = false;

    public float dashTime;
    public float attack1Time;
    public float attack2Time;
    public float attack3Time;
    public float jetPackTime;
    
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
    public const string JUMP = "PlayerJump";
    public const string RUN = "PlayerRun";
    public const string RUN_SHOTGUN = "PlayerRunShotgun";
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
        
        if (blockAction) return;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
            changeAnimationState(JUMP);
        }
        if (Input.GetButtonDown("Dash"))
        {
            dash();
            changeAnimationState(DASH);
        }

        if (Input.GetButtonDown("JetPack"))
        {
            jetPack();
            changeAnimationState(JETPACK);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            attack1();
            changeAnimationState(ATTACK1);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            attack2();
            changeAnimationState(ATTACK2);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            attack3();
            changeAnimationState(ATTACK3);
        }
        
        movePlayer();
        basicAnimation();
        FlipPlayerToMouse();
    }

    void movePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        
    }

    //**********************************************************************************************************************
    //Player Actions 
    //**********************************************************************************************************************
    
    void jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));
    }

    void dash()
    {
        StartCoroutine(BlockActionsCoroutine(dashTime));
        if (isFacingRight)
            rb.AddForce(new Vector2(dashForce, 0f));
        else
            rb.AddForce(new Vector2(-dashForce, 0f));
    }
    
    void jetPack()
    {
        StartCoroutine(BlockActionsCoroutine(jetPackTime));
    }
    
    void attack1()
    {
        StartCoroutine(BlockActionsCoroutine(attack1Time));
    }
    
    void attack2()
    {
        StartCoroutine(BlockActionsCoroutine(attack2Time));
    }
    
    void attack3()
    {
        StartCoroutine(BlockActionsCoroutine(attack3Time));
    }
    
    
    //**********************************************************************************************************************
    //Health  
    //**********************************************************************************************************************
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
        //TODO
        Debug.Log("Player took " + damage + " damage.");
        changeAnimationState(HURT);
        BlockActionsCoroutine(20);
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            healthBar.setHealth(currentHealth);
        }
    }
    
    //**********************************************************************************************************************
    //Utils
    //**********************************************************************************************************************

    private IEnumerator BlockActionsCoroutine(float time)
    {
        blockAction = true;
        yield return new WaitForSeconds(time);
        blockAction = false;
    }
    
    //**********************************************************************************************************************
    //Animations / Sprite Rendering
    //**********************************************************************************************************************

    void basicAnimation()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (isGrounded)
        {
            if (horizontalMovement != 0)
            {
                changeAnimationState(RUN);
            }
            else
            {
                changeAnimationState(IDLE);
            }
        }
        else
        {
            changeAnimationState(FALLING);
        }
    }
    
    public void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }

    // Sets the player's orientation based on the given boolean value.
    // If faceRight is true, the player will face right; otherwise, the player will face left.
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