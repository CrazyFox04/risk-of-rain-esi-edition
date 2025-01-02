using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private int row;
    private int column;
    private int id = -1;
    public GameObject[] enemies;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void set(int row, int column, int id)
    {
        this.row = row;
        this.column = column;
        this.id = id;
    }
    
    void Update()
    {
        if (id == -1) return;
        if (player == null)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (Exception e)
            {
                return;
            }
        }
        if (Vector2.Distance(player.position, transform.position) < 15)
        {
           useSpawner();
        }
    }
    
    private void useSpawner()
    {
        int enemyId = gameController.IfCanSpawnCurrentLevelSpawnAt(row, column, id);
        if (enemyId != -1)
        {
            int type = gameController.GetCharacterType(enemyId);
            switch (type)
            {
                case 1:
                    Instantiate(enemies[0], transform.position, Quaternion.identity).GetComponent<Enemy>()
                        .set(enemyId, 5);
                    break;
                case 2:
                    Instantiate(enemies[1], transform.position, Quaternion.identity).GetComponent<Enemy>()
                        .set(enemyId, 6);
                    break;
            }
        }
    }
}
