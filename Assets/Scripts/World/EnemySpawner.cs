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
        if (id != -1)
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
                        Instantiate(enemies[0], transform.position, Quaternion.identity).GetComponent<Enemy>().set(tempId, 5);
                        break;
                    case 2:
                        Instantiate(enemies[1], transform.position, Quaternion.identity).GetComponent<Enemy>().set(tempId, 6);
                        break;
                }
                
            }
            
        }
    }
}
