using System.Collections;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public MovementPlayer movementPlayer;
    public SpriteRenderer spriteRenderer; // Ajoutez cette ligne

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
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth < 0)
        {
            currentHealth = 0;
            healthBar.setHealth(currentHealth);
        }
        if (movementPlayer != null)
        {
            movementPlayer.weakJump();
        }
        StartCoroutine(ChangeColorTemporarily(new Color(1.0f, 0.3f, 0.3f), 0.3f));
    }

    private IEnumerator ChangeColorTemporarily(Color color, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}