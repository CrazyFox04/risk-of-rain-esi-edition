using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public Transform[] points;
    private int pointIndex = 0;
    private Transform currentPoint;

    public GameObject player;
    private HealthPlayer healthPlayer;

    public int damage = 10;
    public float damageInterval = 1.0f;
    private bool isPlayerInContact = false;

    void Start()
    {
        currentPoint = points[pointIndex];
        healthPlayer = player.GetComponent<HealthPlayer>();
    }

    void FixedUpdate()
    {
        Vector3 dir = currentPoint.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(currentPoint.position.x, 0, currentPoint.position.z)) < 0.3f)
        {
            pointIndex = (pointIndex + 1) % points.Length;
            currentPoint = points[pointIndex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerInContact = true;
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerInContact = false;
            StopCoroutine(ApplyDamageOverTime());
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (isPlayerInContact)
        {

            if (healthPlayer != null)
            {
                healthPlayer.takeDamage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
