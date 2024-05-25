public interface IState<T>
{
    public void Enter();
    public void Execute();
    public void LateExecute();
    public void Sleep();
    void AddTransition(T input, IState<T> state);
    void RemoveTransition(T input);
    void RemoveTransition(IState<T> state);
    IState<T> GetTransition(T input);
    FiniteStateMachine<T> SetFSM { set; }
}
