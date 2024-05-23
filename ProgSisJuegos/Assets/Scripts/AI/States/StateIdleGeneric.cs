using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleGeneric<T> : StateBase<T>
{
    private EnemyBase _controller;

    public StateIdleGeneric(EnemyBase controller)
    {
        _controller = controller;
    }

    public override void Enter()
    {
        if (!_controller.IsIdling)
            _controller.StartIdle();
    }
}
