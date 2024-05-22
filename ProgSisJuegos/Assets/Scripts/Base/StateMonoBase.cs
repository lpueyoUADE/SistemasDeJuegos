using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMonoBase<T> : MonoBehaviour, IState<T>
{
    protected FiniteStateMachine<T> _fsm;
    Dictionary<T, IState<T>> _transitions = new Dictionary<T, IState<T>>();

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void LateExecute() { }
    public virtual void Sleep() { }

    public void AddTransition(T input, IState<T> state) 
    {
        _transitions[input] = state;
    }

    public void RemoveTransition(T input)
    {
        if (_transitions.ContainsKey(input)) _transitions.Remove(input);
    }

    public void RemoveTransition(IState<T> state)
    {
        foreach (var item in _transitions)
        {
            T key = item.Key;
            IState<T> value = item.Value;

            if (value == state)
            {
                _transitions.Remove(key);
                break;
            }
        }
    }

    public IState<T> GetTransition(T input)
    {
        if (_transitions.ContainsKey(input))
            return _transitions[input];

        return null;
    }

    public FiniteStateMachine<T> SetFSM { set { _fsm = value; } }
}
