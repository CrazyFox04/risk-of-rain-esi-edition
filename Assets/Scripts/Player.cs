using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public int id;
    
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
    public float jetPackSpeed;
    public float hurtTime;
    public float landingTime;
    

    public float dashTime;
    public float attack1Time;
    public float attack2Time;
    public float attack3Time;
    public float attack4Time;
    public float attack5Time;
    
    public float attack1ChargeTime;
    public float attack2ChargeTime;
    public float attack3ChargeTime;
    public float attack4ChargeTime;
    public float attack5ChargeTime;
    
    public float jetPackTime;

    public bool canAttack1;
    public bool canAttack2;
    public bool canAttack3;
    public bool canAttack4;
    public bool canAttack5;
    public bool canDash;
    public bool canJetPack;
    public bool canRun;
    
    public int maxHealth = 100;
    public int currentHealth;

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
    
    GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.TakePlayerDamage(10);

        
        id = gameController.GetPlayerId();
        maxHealth = gameController.GetCharacterMaxHealth(id);
        Debug.Log("Player max health: " + maxHealth);
        currentHealth = gameController.GetCharacterHealth(id);
        Debug.Log("Player current health: " + currentHealth);
        dashForce = (float)gameController.GetPlayerDashForce();
        jetPackSpeed = (float)gameController.GetJetPackForce();
        
        isBussy = gameController.IsCharacterBusy(id);
        hurtTime = (float)gameController.GetCharacterHurtTime(id);
        landingTime = (float)gameController.GetPlayerLandingTime();
        dashTime = (float)gameController.GetPlayerDashTime();
        // attack1Time = (float)gameController.GetCharacterAttackTime(id, "ATTACK1");
        // attack2Time = (float)gameController.GetCharacterAttackTime(id, "ATTACK2");
        // attack3Time = (float)gameController.GetCharacterAttackTime(id, "ATTACK3");
        // attack4Time = (float)gameController.GetCharacterAttackTime(id, "ATTACK4");
        // attack5Time = (float)gameController.GetCharacterAttackTime(id, "ATTACK5");
        
        jetPackTime = (float)gameController.GetJetPackMaxTime();
        
        moveSpeed = (float)gameController.GetCharacterSpeed(id);
        jumpForce = (float)gameController.GetCharacterJumpForce(id);
        
        isBussy = gameController.IsCharacterBusy(id);
        isJetPacking = gameController.IsPlayerUsingJetpack();
        isDashing = gameController.IsPlayerDashing();
        
        // canAttack1 = gameController.CanCharacterAttack(id, "ATTACK1");
        // canAttack2 = gameController.CanCharacterAttack(id, "ATTACK2");
        // canAttack3 = gameController.CanCharacterAttack(id, "ATTACK3");
        // canAttack4 = gameController.CanCharacterAttack(id, "ATTACK4");
        // canAttack5 = gameController.CanCharacterAttack(id, "ATTACK5");
        // canDash = gameController.CanCharacterMove(id, "DASH");
        // canJetPack = gameController.CanCharacterMove(id, "JETPACK");
        // canRun = gameController.CanCharacterMove(id, "RUN");
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
            startJetPack();
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
    
    public void startClimbing()
    {
        isClimbing = true;
    }
    
    public void stopClimbing()
    {
        isClimbing = false;
    }

    void startJetPack()
    {
        isJetPacking = true;
    }
    void stopJetPack()
    {
        isJetPacking = false;
    }
    
    void movePlayer()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);
        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * moveSpeed);
        } else if (isJetPacking)
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
    
    void attack4()
    {
        //Call model here
        doAttackByDistance(3);
        StartCoroutine(BlockActions(attack4Time));
        StartCoroutine(performAnimation(ATTACK4, attack4Time));
    }
    
    void attack5()
    {
        //Call model here
        doAttackByDistance(3);
        StartCoroutine(BlockActions(attack5Time));
        StartCoroutine(performAnimation(ATTACK5, attack5Time));
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
        // healthBar.setMaxHealth(maxHealth);
    }

    public void addHealth(int health)
    {
        //Call model here
        // healthBar.setHealth(currentHealth);
    }

    public void takeDamage(int damage)
    {
        //Call model here
        // Debug.Log("Player took damage: " + damage);
        StartCoroutine(performAnimation(HURT, hurtTime));
        // healthBar.setHealth(currentHealth);
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
        
        
        if (isJetPacking)
        {
            changeAnimationState(JETPACK);   
        } else if (Input.GetAxis("Vertical") != 0 && isClimbing)
        {
            changeAnimationState(CLIMB);   
        } else if (!isGrounded)
        {
            changeAnimationState(FALLING);
        } else if (Input.GetAxis("Horizontal") != 0)
        {
            changeAnimationState(RUN);
        } else
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

}