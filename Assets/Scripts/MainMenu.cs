using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playConfigurationSceneName;
	public string settingsLevelSceneName;

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
