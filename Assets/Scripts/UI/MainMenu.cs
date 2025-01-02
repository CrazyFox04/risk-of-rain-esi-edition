using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuScene
{
    [SerializeField] private string playConfigurationSceneName;
	[SerializeField] private string settingsLevelSceneName;

    public void StartGame()
    {
        SceneLoader.LoadSceneIfExists(playConfigurationSceneName);
    }

    public void OpenOptions()
    {
       SceneLoader.LoadSceneIfExists(settingsLevelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
