using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.LoadLevel1();
    }
}
