using UnityEngine;

public class UIPauseScreen : MonoBehaviour
{

    public void Show()
    {
        Time.timeScale = 0;
        GameManager.Instance.CurrentPlayer.DisableActing();
        gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.CurrentPlayer.EnableActing();
        gameObject.SetActive(false);
    }
}
