using UnityEngine;

[CreateAssetMenu(fileName = "LoadingGameGameState", menuName = "Scriptable Objects/LoadingGameGameState")]
public class LoadingGameGameState : GameState
{
    public override void OnStateActivated(GameState state)
    {
        base.OnStateActivated(state);
        AsyncOperation operation = SceneManagementUtils.LoadLevel1();
        UIManager.Instance.ShowLoadingScreen(operation);
        EventManager.OnLoadingFinished.AddListener(OnLoadingFinished);
    }

    private void OnLoadingFinished()
    {
        EventManager.OnLoadingFinished.RemoveListener(OnLoadingFinished);
        GameManager.Instance.OnMainGameLoaded();
    }
}
