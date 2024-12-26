using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public bool isGrounded = true;
    public bool isFacingRight = true;

    private bool isDashing = false;
    private bool isJetPacking = false;
    private bool isPerformingAnimation = false;
    public bool isBussy = false;
    public bool isClimbing = false;
    
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
    
    public List<AbstractEnemy> enemies = new List<AbstractEnemy>();

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
    public const string CLIMB = "PlayerClimbing";

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
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
    
    //-----------------Player Movement-----------------

    void movePlayer()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);
        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * moveSpeed);
        }
    }
    
    //-----------------Player Actions-----------------

    void jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void dash()
    {
        StartCoroutine(BlockActions(dashTime));
        StartCoroutine(performAnimation(DASH, dashTime));
        if (isFacingRight)
            rb.AddForce(new Vector2(dashForce, 0f));
        else
            rb.AddForce(new Vector2(-dashForce, 0f));
    }

    void jetPack()
    {
        StartCoroutine(performAnimation(JETPACK, jetPackTime));
        //better without
        // StartCoroutine(BlockActions(jetPackTime));
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
        //Call model here
        doAttackByDistance(1);
        StartCoroutine(BlockActions(attack1Time));
        StartCoroutine(performAnimation(ATTACK1, attack1Time));
    }

    void attack2()
    {
        //Call model here
        doAttackByDistance(3);
        StartCoroutine(BlockActions(attack2Time));
        StartCoroutine(performAnimation(ATTACK2, attack2Time));
        
    }

    void attack3()
    {
        //Call model here
        doAttackByDistance(3);
        StartCoroutine(BlockActions(attack3Time));
        StartCoroutine(performAnimation(ATTACK3, attack3Time));
    }

    void doAttackByDistance(float maxDistance)
    {
        foreach (AbstractEnemy enemy in enemies)
        {
            float distance = transform.position.x - enemy.transform.position.x;
            if( isFacingRight  && distance < 0 && distance > -maxDistance)
            {
                enemy.takeDamage(10);
            }
            else if (!isFacingRight && distance > 0 && distance < maxDistance)
            {
                enemy.takeDamage(10);
            }
        }
    }
    
    //-----------------Health-----------------
    
    public void addMaxHealth(int health)
    {
        //Call model here
        healthBar.setMaxHealth(maxHealth);
    }

    public void addHealth(int health)
    {
        //Call model here
        healthBar.setHealth(currentHealth);
    }

    public void takeDamage(int damage)
    {
        //Call model here
        // Debug.Log("Player took damage: " + damage);
        StartCoroutine(performAnimation(HURT, hurtTime));
        healthBar.setHealth(currentHealth);
    }
    
    
    //-----------------Collision Behaviors-----------------
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(performAnimation(LANDING, landingTime));
        }
    }
    
    //Enter in hit collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AbstractEnemy enemy = other.GetComponent<AbstractEnemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
    }
    
    //Exit from hit collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AbstractEnemy enemy = other.GetComponent<AbstractEnemy>();
            if (enemy != null)
            {
                enemies.Remove(enemy);
            }
        }
    }
    
    //-----------------todo - remove-----------------
    //CAN BE REPLACED BY MODEL !!!!!!!
    private IEnumerator BlockActions(float time)
    {
        isBussy = true;
        yield return new WaitForSeconds(time);
        isBussy = false;
    }
    
    //-----------------Animations-----------------
    void updateBaseAnimation()
    {
        if (isPerformingAnimation) return;
        
        if (!isGrounded)
        {
            changeAnimationState(FALLING);
        }
        else if (Input.GetAxis("Vertical") != 0 && isClimbing)
        {
            changeAnimationState(CLIMB);   
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            changeAnimationState(RUN);
        }
        else
        {
            changeAnimationState(IDLE);
        }
        
    }
    
    private IEnumerator performAnimation(string animation, float attackTime)
    {
        isPerformingAnimation = true;
        changeAnimationState(animation);
        yield return new WaitForSeconds(attackTime);
        isPerformingAnimation = false;
    }

    public void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }

    void SetOrientation(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
        isFacingRight = faceRight;
    }

    void FlipPlayerToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x)
        {
            SetOrientation(true);
        }
        else
        {
            SetOrientation(false);
        }
    }
//-----------------Externals-----------------
    public void startClimbing()
    {
        isClimbing = true;
    }
    
    public void stopClimbing()
    {
        isClimbing = false;
    }
}

