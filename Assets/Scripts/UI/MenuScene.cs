using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    [SerializeField] private string parentSceneName;

    public void PreviousScene()
    {
        SceneLoader.LoadSceneIfExists(parentSceneName);
    }
}
