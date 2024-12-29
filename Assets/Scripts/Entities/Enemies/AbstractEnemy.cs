using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour
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
    
    
    public const string IDLE = "Idle";
    public const string ATTACK = "Attack";
    public const string RUN = "Run";
    public const string HURT = "Hurt";

    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void FixedUpdate()
    {
        if (id == -1) return;
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move();
        tryAttack();
        flip();
        updateBaseAnimation();
        Debug.Log(gameController.GetCharacterHealth(id));
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

    //-----------------Movement-----------------
    protected virtual void move()
    {
        // if (gameController.CanCharacterMove_RUN(id))
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
    
    // public void jump()
    // {
    //     Debug.Log("Jump");
    //     if (gameController.CanCharacterMove(id, 1) && rb.velocity.x == 0)
    //     {
    //         rb.AddForce(new Vector2(0f, (float)gameController.GetCharacterJumpForce(id)));
    //     }
    // }
    
    
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
    
    //-----------------Health-----------------
    //CAN BE REPLACED BY MODEL ?
    // public void takeDamage(int damage)
    // {
    //     //Call model here
    //     StartCoroutine(performAnimation(HURT, hurtTime));
    // }
    
    
    //-----------------Attack-----------------

    protected void tryAttack()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        // Debug.Log(gameController.CanCharacterAttack(id,attackIndex));
        if (distance < gameController.GetEnemyAttackRange(id) && gameController.CanCharacterAttack(id, attackIndex))
        {
            // StartCoroutine(BlockActions(chargeTime));
            StartCoroutine(chargeAttack());
        }
    }
    
    protected IEnumerator chargeAttack()
    {
        StartCoroutine(performAnimation(ATTACK, (float)gameController.GetCharacterAttackTime(id, attackIndex)));
        // StartCoroutine(BlockActions((float)gameController.GetCharacterAttackTime_ATTACK1(id)));
        yield return new WaitForSeconds((float)gameController.GetAttackChargeTime(id,attackIndex));
        attack();
    }
    
    protected void attack()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        //TODO
        //can attack
        if (distance < gameController.GetEnemyAttackRange(id) && gameController.CanCharacterAttack(id,attackIndex))
        {
            // gameController.Attack(id, attackIndex, gameController.GetPlayerId());
        }
    }
    
    
//CAN BE REPLACED BY MODEL !!!!!!!
    // protected IEnumerator coolDownWait()
    // {
    //     canAttack = false;
    //     yield return new WaitForSeconds(coolDown);
    //     canAttack = true;
    // }
    
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
}