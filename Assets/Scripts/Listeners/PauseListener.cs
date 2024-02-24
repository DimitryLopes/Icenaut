using System.Collections;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ShowPauseScreen();
        }
    }
}
