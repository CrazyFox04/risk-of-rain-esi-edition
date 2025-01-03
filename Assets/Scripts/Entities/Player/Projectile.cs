using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int attackType;
    private int playerId;
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D rb;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    

    public void Initialize(int playerId, int attackType, bool isFacingRight)
    {
        this.playerId = playerId;
        this.attackType = attackType;
        if (!isFacingRight)
        {
            force = -force;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
        StartCoroutine(DestroyAfterTime(3f));
    }
    
    //to prevent bugs
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        await SemaphoreManager.Semaphore.WaitAsync();
        try
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null && gameController.CanCharacterAttack(playerId, attackType))
                {
                    gameController.Attack(playerId, attackType, enemy.getId());
                }
            }
            else if(gameController.CanCharacterAttack(playerId, attackType))
            {
                gameController.Attack(playerId, attackType, -1);
            }
            Destroy(gameObject);
        }
        finally
        {
            SemaphoreManager.Semaphore.Release();
        }
    }
}