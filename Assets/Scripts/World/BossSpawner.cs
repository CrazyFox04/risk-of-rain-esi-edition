using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private int row;
    private int column;
    private int id = -1;
    public bool isActive = false;
    public GameObject[] enemies;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void set(int row, int column, int id)
    {
        this.row = row;
        this.column = column;
        this.id = id;
    }
    
    void Update()
    {
        if (Vector2.Distance(player.position, transform.position) < 15 && Input.GetButtonDown("Use") && gameController.CanActivateBossSpawn(row, column, id))
        {
            isActive = true;
            int tempId = gameController.ActivateBossSpawn(row, column, id);
            Instantiate(enemies[2], transform.position, Quaternion.identity).GetComponent<AbstractEnemy>().set(tempId, 7);
        }

        if (id != -1 && isActive)
        {
            useSpawner();
        }
    }
    
    private void useSpawner()
    {
        if (Vector2.Distance(player.position, transform.position) < 15)
        {
            int tempId = gameController.IfCanSpawnCurrentLevelSpawnAt(row, column, id);
            if (tempId != -1)
            {
                int type = gameController.GetCharacterType(tempId);
                switch (type)
                {
                    case 1:
                        Instantiate(enemies[0], transform.position, Quaternion.identity).GetComponent<AbstractEnemy>().set(tempId, 5);
                        break;
                    case 2:
                        Instantiate(enemies[1], transform.position, Quaternion.identity).GetComponent<AbstractEnemy>().set(tempId, 6);
                        break;
                }
                
            }
            
        }
    }
}