public interface IStateListener<T> where T : GameState
{
    public void RegisterState<U>() where U : T;
}
