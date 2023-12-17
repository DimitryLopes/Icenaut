using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image loadingProgressBar;
    [SerializeField]
    private TextMeshProUGUI loadingProgressText;

    private bool isLoading;
    private AsyncOperation currentOperation;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(AsyncOperation operation)
    {
        currentOperation = operation;
        gameObject.SetActive(true);
        isLoading = true;
    }

    private void Update()
    {
        if (isLoading)
        {
            float progress = currentOperation.isDone ? 1 : currentOperation.progress;
            loadingProgressBar.fillAmount = progress;
            loadingProgressText.text = (progress * 100).ToString();

            if (progress == 1)
            {
                isLoading = false;
                currentOperation = null;
                EventManager.OnLoadingGameFinished.Invoke();
                Hide();
            }
        }
    }

    private void Hide()
    {
        DoHideAnimation();
    }

    private void DoHideAnimation()
    {
        Close();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
