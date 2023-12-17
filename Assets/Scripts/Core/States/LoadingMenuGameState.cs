using UnityEngine;

[CreateAssetMenu(fileName = "LoadingMenuGameState", menuName = "Scriptable Objects/LoadingMenuGameState")]
public class LoadingMenuGameState : GameState
{
    public override void OnStateActivated(GameState state)
    {
        base.OnStateActivated(state);
        AsyncOperation operation = SceneManagementUtils.LoadMainMenu();
        UIManager.Instance.ShowLoadingScreen(operation);
        EventManager.OnLoadingGameFinished.AddListener(OnLoadingFinished);
    }

    private void OnLoadingFinished()
    {
        EventManager.OnLoadingGameFinished.RemoveListener(OnLoadingFinished);
        GameManager.Instance.OnMainGameLoaded();
    }

}