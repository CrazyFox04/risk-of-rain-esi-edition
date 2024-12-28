using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public int id;
    
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.1f;
    public bool isGrounded;
    public bool isFacingRight = true;


    private bool isPerformingAnimation = false;
    public bool isBussy = false;
    public bool isClimbing = false;
    public bool isJetPacking = false;
    
    //TODO
    //Move to model
    public float attack1Range = 1;
    public float attack2Range = 3;
    public float attack3Range = 3;
    public float attack4Range = 3;
    public float attack5Range = 3;
    
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

        
        id = gameController.GetPlayerId();
        updateModel();

        Debug.Log(gameController.IfCanSpawnCurrentLevelSpawnAt(1, 1, 1));

    }

    void Update()
    {
        updateModel();
        CheckIfGrounded();

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
    
    //-----------------Update model-----------------
    private void updateModel()
    {
    }
    
    //-----------------Player Movement-----------------
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
    
    public void startClimbing()
    {
        isClimbing = true;
    }
    
    public void stopClimbing()
    {
        isClimbing = false;
    }
    

    void movePlayer()
    {
        if (gameController.CanCharacterMove(id, 0))
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * (float)gameController.GetCharacterSpeed(id), rb.linearVelocity.y);
        }
        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * (float)gameController.GetCharacterSpeed(id));
        }
        else if (gameController.IsPlayerUsingJetpack())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * (float)gameController.GetJetPackForce());
        }
    }
    
    //-----------------Player Actions-----------------

    void jump()
    {
        if (gameController.CanCharacterMove(id, 1))
        {
            gameController.Move(id, 1);
            rb.AddForce(new Vector2(0f, (float)gameController.GetCharacterJumpForce(id)), ForceMode2D.Impulse);
        }
    }
    
    //TODO
    void jetPack()
    {
        
        if (gameController.CanCharacterMove(id, 3))
        {
            isJetPacking = true;
            gameController.Move(id, 3);
        }
    }

    void dash()
    {
        if (gameController.CanCharacterMove(id, 2))
        {
            gameController.Move(id, 2);
            
            StartCoroutine(performAnimation(DASH, (float)gameController.GetPlayerDashTime()));
            
            float dashForce = (float)gameController.GetPlayerDashForce();
            if (isFacingRight)
                rb.AddForce(new Vector2(dashForce, 0f));
            else
                rb.AddForce(new Vector2(-dashForce, 0f));
            Debug.Log(gameController.CanCharacterMove(id, 2));
        }
    }

    void attack1()
    {
        if (gameController.CanCharacterAttack(id, 0))
        {
            doAttackByDistance(0);
            // StartCoroutine(BlockActions(attack1Time));
            StartCoroutine(performAnimation(ATTACK1, (float)gameController.GetCharacterAttackTime(id, 0)));
        }
    }

    void attack2()
    {
        if (gameController.CanCharacterAttack(id, 1))
        {
            doAttackByDistance(1);
            // StartCoroutine(BlockActions(attack2Time));
            StartCoroutine(performAnimation(ATTACK2, (float)gameController.GetCharacterAttackTime(id, 1)));
        }
    }

    void attack3()
    {
        if (gameController.CanCharacterAttack(id, 2))
        {
            doAttackByDistance(2);
            // StartCoroutine(BlockActions(attack3Time));
            StartCoroutine(performAnimation(ATTACK3, (float)gameController.GetCharacterAttackTime(id, 2)));
        }
    }
    
    void attack4()
    {
        if (gameController.CanCharacterAttack(id, 3))
        {
            doAttackByDistance(3);
            // StartCoroutine(BlockActions(attack4Time));
            StartCoroutine(performAnimation(ATTACK4, (float)gameController.GetCharacterAttackTime(id, 3)));
        }
    }
    
    void attack5()
    {
        if (gameController.CanCharacterAttack(id, 4))
        {
            doAttackByDistance(4);
            // StartCoroutine(BlockActions(attack5Time));
            StartCoroutine(performAnimation(ATTACK5, (float)gameController.GetCharacterAttackTime(id, 4)));
        }
    }

    void doAttackByDistance(int attackType)
    {
        float maxDistance = 0;
        switch (attackType)
        {
            case 0:
                maxDistance = attack1Range;
                break;
            case 1:
                maxDistance = attack2Range;
                break;
            case 2:
                maxDistance = attack3Range;
                break;
            case 3:
                maxDistance = attack4Range;
                break;
            case 4:
                maxDistance = attack5Range;
                break;
        }
        
        foreach (AbstractEnemy enemy in enemies)
        {
            float distance = transform.position.x - enemy.transform.position.x;
            //TODO
            //simplify this
            if( isFacingRight  && distance < 0 && distance > -maxDistance)
            {
                // gameController.Attack(id, attackType, enemy.getId());
            }
            else if (!isFacingRight && distance > 0 && distance < maxDistance)
            {
                // gameController.Attack(id, attackType, enemy.getId());
                // enemy.takeDamage(10);
            }
        }
        
        gameController.Attack(id, attackType, -1);
        
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

    // public void takeDamage(int damage)
    // {
    //     //Call model here
    //     // Debug.Log("Player took damage: " + damage);
    //     StartCoroutine(performAnimation(HURT, hurtTime));
    //     // healthBar.setHealth(currentHealth);
    // }
    
    
    //-----------------Collision Behaviors-----------------
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            gameController.LandCharacter(id);
            StartCoroutine(performAnimation(LANDING, (float)gameController.GetPlayerLandingTime()));
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
    // private IEnumerator BlockActions(float time)
    // {
    //     isBussy = true;
    //     yield return new WaitForSeconds(time);
    //     isBussy = false;
    // }
    
    //-----------------Animations-----------------
    void updateBaseAnimation()
    {
        if (isPerformingAnimation) return;
        
        //TODO
        //jetpack
        if (false)
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
        if (!isPerformingAnimation)
        {
            isPerformingAnimation = true;
            changeAnimationState(animation);
            yield return new WaitForSeconds(attackTime);
            isPerformingAnimation = false;
        }
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