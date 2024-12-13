using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class EnemyAttackPlayer : MonoBehaviour
{
    public Transform player;
    [FormerlySerializedAs("healthPlayer")] public Player playerScript;

    public float attackRange;
    public float coolDown;
    public bool canAttack = true;
    public int damage;
    public float delayOfAttack; //to attack at the right moment with the animation

    public Animator animator;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<Player>();
    }
    
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
        playerScript.takeDamage(damage);
    }
    
    

}