using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LoadingScreen loadingScreen;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void ShowLoadingScreen(AsyncOperation operation)
    {
        loadingScreen.Show(operation);
    }
}
