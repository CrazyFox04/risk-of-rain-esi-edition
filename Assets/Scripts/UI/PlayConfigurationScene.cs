using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayConfiguration : MenuScene
{
    public string initializerScene;
    public string blabla;
    public void PlayGame()
    {
        SceneLoader.LoadSceneIfExists(initializerScene);
    }

    public void Blabla() {
        SceneLoader.LoadSceneIfExists(blabla);
    }
}
