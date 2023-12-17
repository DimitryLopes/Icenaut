using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private GameStatesData statesData;

    public Dictionary<GameStates, GameState> StatesDatabase = new Dictionary<GameStates, GameState>();

    private GameState CurrentState;

    public void Awake()
    {
        GenerateStateDatabase();
        InitializeStateListeners();
    }

    public void ChangeState(GameStates newState)
    {
        if (CurrentState != null)
        {
            CurrentState.DeactivateState();
            EventManager.OnStateDeactivated.Invoke(CurrentState);
        }
        GameState nextState = StatesDatabase[newState];
        nextState.ActivateState();
        CurrentState = nextState;
    }

    private void GenerateStateDatabase()
    {
        foreach(GameState state in statesData.GameStates)
        {
            StatesDatabase.Add(state.EnumID, state);
        }
    }

    private void InitializeStateListeners()
    {
        Assembly assembly = Assembly.GetExecutingAssembly(); 

        var stateListeners = assembly.GetTypes().Where(type => type.GetInterfaces().Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IStateListener<>) &&
                i.GetGenericArguments()[0].IsSubclassOf(typeof(GameState))))
            .Select(type => Activator.CreateInstance(type))
            .ToList();

        foreach (var listener in stateListeners)
        {
            MethodInfo registerStateMethod = listener.GetType().GetMethod("RegisterState");
            if (registerStateMethod != null)
            {
                Type genericArgument = listener.GetType().GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStateListener<>))?
                    .GetGenericArguments()[0];

                MethodInfo genericRegisterStateMethod = registerStateMethod.MakeGenericMethod(genericArgument);
                genericRegisterStateMethod.Invoke(listener, null);
            }
            else
            {
                Debug.LogError("MethodInfo for " + listener.GetType().ToString() + "not found!");
            }
        }


    }

}
public enum GameStates
{
    OnGoing,
    Paused,
    LoadingGame,
    LoadingMenu,
    Finished,
    MainMenu,
}