using UnityEngine;

public abstract class ActivatedInGame : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        EventManager.OnStateActivated.AddListener(OnGameStarted);
    }

    private void OnGameStarted(GameState state)
    {
        if(state.EnumID == GameStates.OnGoing)
        {
            gameObject.SetActive(true);
            OnGameStarted();
        }
    }

    protected abstract void OnGameStarted();
}
