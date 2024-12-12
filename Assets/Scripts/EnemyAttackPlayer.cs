using UnityEngine;
using System.Collections;

public class EnemyAttackPlayer : MonoBehaviour
{
    public Transform player;
    public HealthPlayer healthPlayer;

    public float attackRange;
    public float coolDown;
    public bool canAttack = true;
    public int damage;
    public float delayOfAttack; //to attack at the right moment with the animation

    public Animator animator;
    void FixedUpdate()
    {
        attack();
    }
    private void attack()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < attackRange && canAttack)
        {
            StartCoroutine(DelayedAttack());
            animateAttack();
            StartCoroutine(coolDownWait());
        }
        
    }


    private void animateAttack()
    {
        animator.SetTrigger("Attack");
    }
    
    private IEnumerator coolDownWait()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
    
    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(delayOfAttack);
        healthPlayer.takeDamage(damage);
    }
    
    

}