using System.Collections;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    takeDamage(20);
        //}
        //if (Input.GetKeyDown(KeyCode.Delete))
        //{
        //    addHealth(20);
        //}
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    addMaxHealth(50);
        //}
    }

    public void addMaxHealth(int health)
    {
        maxHealth += health;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void addHealth(int health)
    {
        currentHealth += health;
        healthBar.setHealth(currentHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.setHealth(currentHealth);
        }
    }

    public void takeDamage(int damage)
    {
        changeAnimationState("PlayerHurt");
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            healthBar.setHealth(currentHealth);
        }
    }

    public void changeAnimationState(string animation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) return;
        animator.Play(animation);
    }
}
