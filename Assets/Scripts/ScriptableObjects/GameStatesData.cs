using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StatesData", menuName = "Scriptable Objects/Game State Data")]
public class GameStatesData : ScriptableObject
{
    [SerializeField]
    private List<GameState> states;

    public List<GameState> GameStates => states;
}
