using UnityEngine;
using UnityEditor;

public class UnityMenus 
{
#if UNITY_EDITOR
    [MenuItem("Debug/Play From Main Menu #1")]
    public static void PlayFromMainMenu()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/SCN_MainMenu.unity");
    }

    [MenuItem("Debug/Go To Scene1 #2")]
    public static void GoToScene1()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/SCN_Level_1.unity");
    }
#endif
}
