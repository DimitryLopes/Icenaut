using UnityEngine;

[CreateAssetMenu(fileName = "LoadingMenuGameState", menuName = "Scriptable Objects/LoadingMenuGameState")]
public class LoadingMenuGameState : GameState
{
    public override void OnStateActivated(GameState state)
    {
        base.OnStateActivated(state);
        AsyncOperation operation = SceneManagementUtils.LoadMainMenu();
        UIManager.Instance.ShowLoadingScreen(operation);
        EventManager.OnLoadingFinished.AddListener(OnLoadingFinished);
    }

    private void OnLoadingFinished()
    {
        EventManager.OnLoadingFinished.RemoveListener(OnLoadingFinished);
        GameManager.Instance.OnMainGameLoaded();
    }

}