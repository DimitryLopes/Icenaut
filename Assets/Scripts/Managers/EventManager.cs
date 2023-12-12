using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<GameState> OnStateActivated = new UnityEvent<GameState>();
    public static UnityEvent<GameState> OnStateDeactivated = new UnityEvent<GameState>();
    public static UnityEvent OnLoadingFinished = new UnityEvent();
}
