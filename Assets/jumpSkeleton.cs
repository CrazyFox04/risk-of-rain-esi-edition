using UnityEngine;

public class jumpSkeleton : MonoBehaviour
{
    private EnemyFollow enemyFollow;
    void Start()
    {
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyFollow != null)
        {
            enemyFollow.jump();
        }
    }
}