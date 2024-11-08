using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public static void LoadSceneIfExists(string sceneName)
    {
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("The scene (" + sceneName + ") doesn't exist");
        }
    }

    public static bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string currentSceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (currentSceneName == sceneName)
            {
                return true;
            }
        }
        return false;
    } 
}