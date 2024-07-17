using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatesEnum
{
    Default,
    Stunned,
    Idle,
    Movement,
    CloseRangedAttack,
    LongRangedAttack,
    Chase,
    Dead,
    Patrol,
    Steering,
    RunAndShoot,
}

public class FiniteStateMachine<T>
{
    IState<T> _current;

    public FiniteStateMachine() { }
    public FiniteStateMachine(IState<T> initialState)
    {
        SetInitialState(initialState);
    }

    public void SetInitialState(IState<T> initialState) 
    {
        _current = initialState;
        _current.Enter();
    }

    public void OnUpdate()
    {
        if (_current != null ) _current.Execute();
    }

    public void OnLateExecute()
    {
        if (_current != null ) _current.LateExecute();
    }

    public void Transition(T input)
    { 
        IState<T> newState = _current.GetTransition(input);

        if (newState != null ) 
        {
            _current.Sleep();
            _current = newState;
            _current.SetFSM = this;
            _current.Enter();
        }
    }

    public IState<T> CurrentState => _current;

}
