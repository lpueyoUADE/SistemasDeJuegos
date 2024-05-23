using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStunnedGeneric<T> : StateBase<T>
{
    private float _stunTime;
    private float _currentStunTime;

    public StateStunnedGeneric(float stunTime)
    {
        _stunTime = stunTime;
    }

    public override void Enter()
    {
        base.Enter();
    }
}
