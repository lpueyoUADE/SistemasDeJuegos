using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleGeneric<T> : StateBase<T>
{
    private EnemyBase _controller;
    private FSMAIBase _machine;

    public StateIdleGeneric(EnemyBase controller, FSMAIBase fsm)
    {
        _controller = controller;
        _machine = fsm;
    }

    public override void Enter()
    {
        if (!_machine.QuestionIsIdling())
        {
            float randTime = Random.Range(_machine.BehaviourData.IdleTimeMin, _machine.BehaviourData.IdleTimeMax);
            _machine.ActionStartIdle(randTime);
        }
    }
}
