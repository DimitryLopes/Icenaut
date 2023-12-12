using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField]
    public GameStates EnumID; //i guess?

    public virtual void OnStateActivated(GameState state)
    {
    }

    public virtual void OnStateDeactivated(GameState state)
    {
    }

    public void ActivateState()
    {
        OnStateActivated(this);
        EventManager.OnStateActivated.Invoke(this);
    }

    public void DeactivateState()
    {
        OnStateDeactivated(this);
        EventManager.OnStateDeactivated.Invoke(this);
    }
}
