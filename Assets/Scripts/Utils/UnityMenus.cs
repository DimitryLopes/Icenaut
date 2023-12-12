using UnityEngine;
using UnityEditor;

public class UnityMenus 
{
#if UNITY_EDITOR
    [MenuItem("Debug/Play From Main Menu")]
    public static void PlayFromMainMenu()
    {
        Debug.Log("Pear");
    }

    [MenuItem("Debug/Go To Scene1 #p")]
    public static void GoToScene1()
    {
        Debug.Log("Hot Pear");
    }
#endif
}
