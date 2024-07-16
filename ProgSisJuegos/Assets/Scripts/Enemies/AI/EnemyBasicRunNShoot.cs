using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicRunNShoot : FSMAIBase
{

    private void Update()
    {
        _fsm?.OnUpdate();
        _root?.Execute();
    }

    private void FixedUpdate()
    {
        _fsm?.OnLateExecute();
    }

    public override void InitializeFSM()
    {
        var runNShoot = new StateRunAndShoot<StatesEnum>(_controller);
        var dead = new StateDeadGeneric<StatesEnum>();

        runNShoot.AddTransition(StatesEnum.Dead, dead);
        dead.AddTransition(StatesEnum.RunAndShoot, runNShoot);

        _fsm = new FiniteStateMachine<StatesEnum>(runNShoot);
    }

    public override void InitializeTree()
    {
        var runnshoot = new TreeActionNode(() => _fsm.Transition(StatesEnum.RunAndShoot));
        var dead = new TreeActionNode(() => _fsm.Transition(StatesEnum.Dead));

        var qIsAlive = new TreeQuestionNode(() =>  !gameObject.activeSelf, runnshoot, dead);
        _root = qIsAlive;
    }
}
