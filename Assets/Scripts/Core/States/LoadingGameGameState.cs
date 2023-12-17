using UnityEngine;

[CreateAssetMenu(fileName = "LoadingGameGameState", menuName = "Scriptable Objects/LoadingGameGameState")]
public class LoadingGameGameState : GameState
{
    public override void OnStateActivated(GameState state)
    {
        base.OnStateActivated(state);
        AsyncOperation operation = SceneManagementUtils.LoadLevel1();
        UIManager.Instance.ShowLoadingScreen(operation);
        EventManager.OnLoadingGameFinished.AddListener(OnLoadingFinished);
    }

    private void OnLoadingFinished()
    {
        EventManager.OnLoadingGameFinished.RemoveListener(OnLoadingFinished);
        GameManager.Instance.OnMainGameLoaded();
    }
}
