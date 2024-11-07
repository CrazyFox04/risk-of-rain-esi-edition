using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public string parentSceneName;

    public void PreviousScene()
    {
        SceneLoader.LoadSceneIfExists(parentSceneName);
    }
}
