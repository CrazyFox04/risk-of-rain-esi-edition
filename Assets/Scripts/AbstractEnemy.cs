using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour
{
    public Transform playerPosition;
    public Player playerScript;
    public float speed;
    public float jumpForce;
    public float followRange;
    public Rigidbody2D rb;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public bool isGrounded = true;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    protected bool isFacingRight;

    public float attackRange;
    public float coolDown;
    public bool canAttack = true;
    public int damage;
    public float delayOfAttack;
    public float attackTime;
    
    protected bool isPerformingAnimation = false;
    
    public Collider2D jumpCollider;
    
    protected bool isBusy = false;

    public float chargeTime;
    public float hurtTime;
    
    
    public const string IDLE = "Idle";
    public const string ATTACK = "Attack";
    public const string RUN = "Run";
    public const string HURT = "Hurt";

    protected virtual void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = playerPosition.GetComponent<Player>();
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move();
        tryAttack();
        flip();
        updateBaseAnimation();
    }

    //-----------------Movement-----------------
    protected virtual void move()
    {
        if (isBusy) return;
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < followRange && distance > attackRange)
        {
            Vector2 directionToTarget = (playerPosition.position - transform.position).normalized * speed;
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
    
    public void jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
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
    public void takeDamage(int damage)
    {
        //Call model here
        StartCoroutine(performAnimation(HURT, hurtTime));
    }
    
    
    //-----------------Attack-----------------

    protected void tryAttack()
    {
        if(isBusy) return;
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < attackRange && canAttack)
        {
            StartCoroutine(BlockActions(chargeTime));
            StartCoroutine(chargeAttack());
        }
    }
    
    protected IEnumerator chargeAttack()
    {
        StartCoroutine(performAnimation(ATTACK, attackTime));
        StartCoroutine(BlockActions(attackTime));
        yield return new WaitForSeconds(chargeTime);
        attack();
    }
    
    protected void attack()
    {
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < attackRange && canAttack)
        {
            playerScript.takeDamage(damage);
            StartCoroutine(coolDownWait());
        }
    }
    
    //-----------------todo - remove-----------------
    //CAN BE REPLACED BY MODEL !!!!!!!
    protected IEnumerator BlockActions(float time)
    {
        isBusy = true;
        yield return new WaitForSeconds(time);
        isBusy = false;
    }
    
//CAN BE REPLACED BY MODEL !!!!!!!
    protected IEnumerator coolDownWait()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
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
}