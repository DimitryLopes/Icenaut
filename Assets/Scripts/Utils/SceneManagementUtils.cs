using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagementUtils
{
    public static AsyncOperation LoadLevel1()
    {
        return SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public static AsyncOperation LoadMainMenu()
    {
        return SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
