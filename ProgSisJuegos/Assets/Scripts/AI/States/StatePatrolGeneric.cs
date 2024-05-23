using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrolGeneric<T> : StateBase<T>
{
    private Action<Vector3, float, ForceMode> _move;

    public StatePatrolGeneric(Action<Vector3, float, ForceMode> movement)
    {
        _move = movement;
    }

    public override void Execute()
    {
        _move.Invoke(new Vector3(1, 0, 0), 5, ForceMode.Force);
    }
}
