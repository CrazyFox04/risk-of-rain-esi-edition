using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private int row;
    private int column;
    private int id = -1;
    
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
        if (Vector2.Distance(player.position, transform.position) < 10)
        {
            gameController.IfCanSpawnCurrentLevelSpawnAt(row, column, id);
        }
    }
}
