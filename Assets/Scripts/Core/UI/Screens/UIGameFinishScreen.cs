using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameFinishScreen : MonoBehaviour
{
    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
