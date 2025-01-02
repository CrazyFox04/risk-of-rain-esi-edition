using UnityEngine;

public class AllReloadActions : MonoBehaviour
{
    private GameController gameController;
    
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ReloadActions[] reloadActions = GetComponentsInChildren<ReloadActions>();
        //For Movement
        
        //Jetpack
        reloadActions[0].set(3, true);
        //Dash
        reloadActions[1].set(2, true);
        
        reloadActions[2].set(gameController.GetTertiaryPlayerAttack());
        reloadActions[3].set(gameController.GetSecondaryPlayerAttack());
        reloadActions[4].set(gameController.GetPrimaryPlayerAttack());
    }
}