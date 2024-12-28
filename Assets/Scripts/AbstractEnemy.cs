using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour
{
    public int id;
    
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
        // playerScript = playerPosition.GetComponent<Player>();
        id = gameController.IfCanSpawnCurrentLevelSpawnAt(0,0,1);
        Debug.Log("Enemy id: " + id);
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move();
        tryAttack();
        flip();
        updateBaseAnimation();
    }
    
    private void updateModel()
    {
        
        
    }

    //-----------------Movement-----------------
    protected virtual void move()
    {
        if (gameController.CanCharacterMove_RUN(id))
        {
            float distance = Vector2.Distance(transform.position, playerPosition.position);
            if (distance < gameController.GetEnemyFollowRange(id) && distance > gameController.GetEnemyFollowRange(id))
            {
                Vector2 directionToTarget = (playerPosition.position - transform.position).normalized * (float)gameController.GetCharacterSpeed(id);
                rb.linearVelocity = new Vector2(directionToTarget.x, rb.linearVelocity.y);
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
    
    public void jump()
    {
        if (gameController.CanCharacterMove_JUMP(id))
        {
            rb.AddForce(new Vector2(0f, (float)gameController.GetCharacterJumpForce(id)));
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
        //TODO
        //can attack
        if (distance < gameController.GetEnemyAttackRange(id) && false)
        {
            // StartCoroutine(BlockActions(chargeTime));
            StartCoroutine(chargeAttack());
        }
    }
    
    protected IEnumerator chargeAttack()
    {
        StartCoroutine(performAnimation(ATTACK, (float)gameController.GetCharacterAttackTime_ATTACK1(id)));
        // StartCoroutine(BlockActions((float)gameController.GetCharacterAttackTime_ATTACK1(id)));
        yield return new WaitForSeconds((float)gameController.GetChargeTime_ATTACK1(id));
        attack();
    }
    
    protected void attack()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        //TODO
        //can attack
        if (distance < gameController.GetEnemyAttackRange(id) && false)
        {
            // playerScript.takeDamage(damage);
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