using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MenuScene
{
    [SerializeField] private string playconfigScene;

    void Start() 
    {

    }
    
    public void ReplayGame()
    {
        SceneLoader.LoadSceneIfExists(playconfigScene);
    }

}
