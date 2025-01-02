using UnityEngine;
using System;

public class BossSpawner : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private int row;
    private int column;
    private int id = -1;
    private int bossId;
    private bool isActive = false;
    private bool isEndGame = false; 
    public Animator animator;
    public GameObject[] enemies;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.Play("BossSpawner");
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

        useBossSpawner();
        
        if (isActive)
        {
            endGame();
            if (isEndGame)
            {
                nextLevel();
            }
            else
            {
                useSpawner();
            }
        }
    }

    private async void useBossSpawner()
    {
        await SemaphoreManager.Semaphore.WaitAsync();
        try
        {
            if (Vector2.Distance(player.position, transform.position) < 7)
            {
                if (Input.GetButtonDown("Use") && gameController.CanActivateBossSpawn(row, column, id))
                {
                    isActive = true;
                    animator.Play("BossSpawnerActivated");
                    bossId = gameController.ActivateBossSpawn(row, column, id);
                    Instantiate(enemies[2], transform.position, Quaternion.identity).GetComponent<Enemy>().set(bossId, 7);
                }
            }
        }
        finally
        {
            SemaphoreManager.Semaphore.Release();
        }
    }
    
    private void useSpawner()
    {
        if (Vector2.Distance(player.position, transform.position) < 15)
        {
            int enemyId = gameController.IfCanSpawnCurrentLevelSpawnAt(row, column, id);
            if (enemyId != -1)
            {
                int type = gameController.GetCharacterType(enemyId);
                switch (type)
                {
                    case 1:
                        Instantiate(enemies[0], transform.position, Quaternion.identity).GetComponent<Enemy>().set(enemyId, 5);
                        break;
                    case 2:
                        Instantiate(enemies[1], transform.position, Quaternion.identity).GetComponent<Enemy>().set(enemyId, 6);
                        break;
                }
                
            }
            
        }
    }
    
    private void endGame()
    {
        if (gameController.CanEndCurrentLevel(bossId))
        {
            isEndGame = true;
            animator.Play("BossSpawnerEndGame");
        }
    }
    
    private void nextLevel()
    {
        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("Next Level");
            gameController.NextLevel(bossId);
        }
    }
}