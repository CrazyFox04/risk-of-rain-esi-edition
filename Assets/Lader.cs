using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Player player;
    private GameController gameController;
    private int playerId;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerId = gameController.GetPlayerId();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameController.CanCharacterMove(playerId, 4))
        {
            gameController.Move(playerId, 4);
            player.startClimbing();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.StopMoving(playerId, 4);
            player.stopClimbing();
        }
    }
    
}
