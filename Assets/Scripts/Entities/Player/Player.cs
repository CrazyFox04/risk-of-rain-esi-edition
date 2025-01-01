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
    private bool isClimbing = false;
    private bool isJetPacking = false;
    private bool canShoot = true;
    private int lastHealthValue;
    
    private float attack1Range = 1;
    private float attack2Range = 3;
    private float attack3Range = 3;
    private float attack4Range = 3;
    private float attack5Range = 3;
    
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    private List<Enemy> enemies = new List<Enemy>();
    
    public GameObject projectilePrefab;
    GameController gameController;
    

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
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
        id = gameController.GetPlayerId();
        lastHealthValue = gameController.GetCharacterHealth(id);
    }

    void Update()
    {
        CheckIfGrounded();

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
        checkForDamageAnimation();
        FlipPlayerToMouse();
    }
    
    
    //-----------------Player Movement-----------------
    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (!isGrounded)
        {
            gameController.TakeOffCharacter(id);
        }
    }

    private void movePlayer()
    {
        if (gameController.CanCharacterMove(id, 0))
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * (float)gameController.GetCharacterSpeed(id), rb.linearVelocity.y);
        }
        if (gameController.IsMoving(id) == 4)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * (float)gameController.GetCharacterSpeed(id));
        }
        else if (gameController.IsMoving(id) == 3 && isJetPacking)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Input.GetAxis("Vertical") * (float)gameController.GetJetPackForce());
        }
    }
    
    //-----------------Player Actions-----------------

    private void jump()
    {
        if (gameController.CanCharacterMove(id, 1))
        {
            gameController.Move(id, 1);
            rb.AddForce(new Vector2(0f, (float)gameController.GetCharacterJumpForce(id)), ForceMode2D.Impulse);
        }

        // for (int i = 0; i <= 5; i++)
        // {
        //     Debug.Log("item " + i + " : " + gameController.getNumberOfItem(id, i));
        // }
        // Debug.Log("item " + 0 + " : " + gameController.getNumberOfItem(id, 0));
    }
    
    private void jetPack()
    {
        if (gameController.IsMoving(id) == 3)
        {
            gameController.StopMoving(id, 3);
            isJetPacking = false;
        } else if (gameController.CanCharacterMove(id, 3))
        {
            gameController.Move(id,3);
            isJetPacking = true;
        }
    }

    private void dash()
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
        }
    }

    private void attack1()
    {
        if (gameController.CanCharacterAttack(id , 0))
        {
            StartCoroutine(chargeAttack(0));
            // doAttackByDistance(0);
            // StartCoroutine(BlockActions(attack1Time));
            StartCoroutine(performAnimation(ATTACK1, (float)gameController.GetCharacterAttackTime(id, 0), true));
        }
    }

    private void attack2()
    {
        
        if (gameController.CanCharacterAttack(id ,1))
        {
            StartCoroutine(chargeAttack(1, false));
            // StartCoroutine(BlockActions(attack2Time));
            StartCoroutine(performAnimation(ATTACK2, (float)gameController.GetCharacterAttackTime(id, 1), true));
        }
    }

    private void attack3()
    {
        if (gameController.CanCharacterAttack(id ,2))
        {
            StartCoroutine(chargeAttack(2, false));
            // doAttackByDistance(2);
            // StartCoroutine(BlockActions(attack3Time));
            StartCoroutine(performAnimation(ATTACK3, (float)gameController.GetCharacterAttackTime(id, 2), true));
        }
    }
    
    private void attack4()
    {
        if (gameController.CanCharacterAttack(id, 3))
        {
            StartCoroutine(chargeAttack(3));
            // doAttackByDistance(3);
            // StartCoroutine(BlockActions(attack4Time));
            StartCoroutine(performAnimation(ATTACK4, (float)gameController.GetCharacterAttackTime(id, 3), true));
        }
    }
    
    private void attack5()
    {
        if (gameController.CanCharacterAttack(id, 4))
        {
            StartCoroutine(chargeAttack(4));
            // StartCoroutine(BlockActions(attack5Time));
            StartCoroutine(performAnimation(ATTACK5, (float)gameController.GetCharacterAttackTime(id, 4), true));
        }
    }
    
    private IEnumerator chargeAttack(int attackType, bool melee = true)
    {
        yield return new WaitForSeconds((float)gameController.GetAttackChargeTime(id, attackType));
        if (melee)
        {
            attackMelee(attackType);
        }
        else if (canShoot)
        {
                attackProjectile(attackType);
        }
    }
    
    private IEnumerator blockShoot(int attackType)
    {
        canShoot = false;
        yield return new WaitForSeconds((float)gameController.GetCharacterAttackTime(id, attackType));
        canShoot = true;
    }
    
    private void attackProjectile(int attackType)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.Initialize(id, attackType, isFacingRight);
        StartCoroutine(blockShoot(attackType));
    }

    private void attackMelee(int attackType)
    {
        if (!gameController.CanCharacterAttack(id, attackType)) return;
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
        foreach (Enemy enemy in enemies)
        {
            float distance = transform.position.x - enemy.transform.position.x;
            
            if( isFacingRight  && distance < 0 && distance > -maxDistance)
            {
                gameController.Attack(id, attackType, enemy.getId());
                return;
            }
            if (!isFacingRight && distance > 0 && distance < maxDistance)
            {
                gameController.Attack(id, attackType, enemy.getId());
                return;
            }
        }
        gameController.Attack(id, attackType, -1);
    }
    
    
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
            Enemy enemy = other.GetComponent<Enemy>();
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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Remove(enemy);
            }
        }
    }
    
    
    //-----------------Animations-----------------
    
    private void updateBaseAnimation()
    {
        int move = gameController.IsMoving(id);
        
        if (isPerformingAnimation) return;
        else if (move == 3)
        {
            changeAnimationState(JETPACK);   
        } else if (move == 4)
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
    
    private IEnumerator performAnimation(string animation, float attackTime, bool force = false)
    {
        if (!isPerformingAnimation || force)
        {
            isPerformingAnimation = true;
            changeAnimationState(animation);
            yield return new WaitForSeconds(attackTime);
            isPerformingAnimation = false;
        }
    }

    private void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }

    private void SetOrientation(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
        isFacingRight = faceRight;
    }

    private void FlipPlayerToMouse()
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
    
    private void checkForDamageAnimation()
    {
        if (gameController.GetCharacterHealth(id) < lastHealthValue)
        {
            StartCoroutine(performAnimation(HURT, (float)gameController.GetCharacterHurtTime(id)));
            lastHealthValue = gameController.GetCharacterHealth(id);
        }
    }

}