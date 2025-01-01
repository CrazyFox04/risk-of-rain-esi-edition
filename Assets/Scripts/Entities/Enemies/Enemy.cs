using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int id;
    public int attackIndex;
    
    public Transform playerPosition;
    public Rigidbody2D rb;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public bool isGrounded;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    protected bool isFacingRight;
    
    protected bool isPerformingAnimation = false;
    
    public Collider2D jumpCollider;

    private GameController gameController;
    
    private int lastHealthValue;
    
    public Transform obstacleCheck;
    public LayerMask obstacleLayer;
    
    
    public const string IDLE = "Idle";
    public const string ATTACK = "Attack";
    public const string RUN = "Run";
    public const string HURT = "Hurt";

    protected void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyHealthBar healthBar = GetComponentInChildren<EnemyHealthBar>();
        healthBar.setEnemyId(id);
        lastHealthValue = gameController.GetCharacterHealth(id);
    }

    protected void FixedUpdate()
    {
        if (id == -1) return;
        //enemy too far from player -> do nothing (not visible on the screen)
        if (Vector2.Distance(playerPosition.position, transform.position) < 25)
        {
            CheckIfGrounded();
            move();
            checkForObstacleAndJump();
            tryAttack();
            flip();
            updateBaseAnimation();
            checkForDamageAnimation();
            isDeath();
        }
    }
    
    public void set(int id, int attackIndex)
    {
        this.id = id;
        this.attackIndex = attackIndex;
    }
    
    public int getId()
    {
       return this.id;
    }
    
    private void isDeath()
    {
        if (gameController.GetCharacterHealth(id) <= 0)
        {
            //TODO
            StartCoroutine(performAnimation("Death", 0.3f));
            StartCoroutine(death());
        }
    }
    
    private IEnumerator death()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    //-----------------Movement-----------------
    protected void move()
    {
       
        if (gameController.CanCharacterMove(id, 0))
        {
            
            float distance = Vector2.Distance(transform.position, playerPosition.position);
            float followRange = (float)gameController.GetEnemyFollowRange(id);
            float stopRange = 1f;
            if (distance < followRange && distance > stopRange)
            {
                Vector2 directionToTarget = (playerPosition.position - transform.position).normalized * (float)gameController.GetCharacterSpeed(id);
                rb.linearVelocity = new Vector2(directionToTarget.x, rb.linearVelocity.y);
            }else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            if (rb.linearVelocity.x > 0.1f)
            {
                isFacingRight = true;
            }
            else if (rb.linearVelocity.x < -0.1f)
            {
                isFacingRight = false;
            }
        }
    }
    
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, obstacleLayer);
        if (!isGrounded)
        {
            gameController.TakeOffCharacter(id);
        } else
        {
            gameController.LandCharacter(id);
        }
    }
    
    private void checkForObstacleAndJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(obstacleCheck.position, isFacingRight ? Vector2.right : Vector2.left, 1f, obstacleLayer);
        if (hit.collider != null)
        {
            jump();
        }
    }
    
    public async void jump()
    {
        await SemaphoreManager.Semaphore.WaitAsync();
        try
        {
            if (gameController.CanCharacterMove(id, 1))
            {
                rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
                gameController.Move(id, 1);
            } 
        }
        finally
        {
            SemaphoreManager.Semaphore.Release();
        }
    }


    protected void flip()
    {
        if (playerPosition.position.x > transform.position.x)
        {
            SetOrientation(true);
        } else
        {
            SetOrientation(false);
        }
    }
    
    
    
    //-----------------Attack-----------------

    protected void tryAttack()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < gameController.GetEnemyAttackRange(id) && gameController.CanCharacterAttack(id, attackIndex))
        {
            StartCoroutine(chargeAttack());
        }
    }
    
    protected IEnumerator chargeAttack()
    {
        StartCoroutine(performAnimation(ATTACK, (float)gameController.GetCharacterAttackTime(id, attackIndex)));
        yield return new WaitForSeconds((float)gameController.GetAttackChargeTime(id,attackIndex));
        attack();
    }
    
    protected async void attack()
    {
        await SemaphoreManager.Semaphore.WaitAsync();
        try
        {
            float distanceX = Mathf.Abs(transform.position.x - playerPosition.position.x);
            float distanceY = Mathf.Abs(transform.position.y - playerPosition.position.y);

            if (distanceX < gameController.GetEnemyAttackRange(id) && gameController.CanCharacterAttack(id,attackIndex) && distanceY < 1)
            {
                gameController.Attack(id, attackIndex, gameController.GetPlayerId());
            }
        }
        finally
        {
            SemaphoreManager.Semaphore.Release();
        }
    }
    
    
    //-----------------Animation-----------------
    
    protected void updateBaseAnimation()
    {
        if (isPerformingAnimation) return;
        float horizontalMovement = rb.linearVelocity.x;
        if (horizontalMovement != 0)
        {
            changeAnimationState(RUN);
        }
        else
        {
            changeAnimationState(IDLE);
        }
    }
    
    protected IEnumerator performAnimation(string animation, float attackTime)
    {
        isPerformingAnimation = true;
        changeAnimationState(animation);
        yield return new WaitForSeconds(attackTime);
        isPerformingAnimation = false;
    }

    protected void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }

    protected void SetOrientation(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
        isFacingRight = faceRight;
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