using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRunAndShoot<T> : StateBase<T>
{
    private EnemyBase _controller;

    public StateRunAndShoot(EnemyBase owner)
    {
        _controller = owner;
    }

    public override void Execute()
    {
        _controller.Fire();
    }

    public override void LateExecute()
    {
        _controller.Move(_controller.transform.forward);
    }
}
