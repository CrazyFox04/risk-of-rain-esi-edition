using UnityEngine;

public class Ladder : MonoBehaviour
{
    private GameController gameController;
    private int playerId;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerId = gameController.GetPlayerId();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameController.CanCharacterMove(playerId, 4))
        {
            gameController.Move(playerId, 4);
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.StopMoving(playerId, 4);
   
        }
    }
    
}
