using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class UnityMenus 
{
#if UNITY_EDITOR
    [MenuItem("Debug/Play From Main Menu #1")]
    public static void PlayFromMainMenu()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/SCN_MainMenu.unity");
    }

    [MenuItem("Debug/Go To Scene1 #2")]
    public static void GoToScene1()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/SCN_Level_1.unity");
    }
#endif
}
