using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MenuScene
{
    [SerializeField] private string playconfigScene;
    private GameController gameController;

    void Start() 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.DestroyGame();
    }
    
    public void ReplayGame()
    {
        SceneLoader.LoadSceneIfExists(playconfigScene);
    }
}
