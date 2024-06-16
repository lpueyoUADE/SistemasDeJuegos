using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMTest : FSMAIBase
{
    public override void InitializeFSM()
    {
        var idle = new StateIdleGeneric<StatesEnum>(_controller, this);
        var patrol = new StatePatrolGeneric<StatesEnum>(_controller, PatrolPoints);
        var dead = new StateDeadGeneric<StatesEnum>();

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Dead, dead);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Dead, dead);

        _fsm = new FiniteStateMachine<StatesEnum>(idle);
    }

    public override void InitializeTree()
    {
        var idle = new TreeActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var patrol = new TreeActionNode(() => _fsm.Transition(StatesEnum.Patrol));
        var dead = new TreeActionNode(() => _fsm.Transition(StatesEnum.Dead));

        // Random test
        var dic = new Dictionary<ITreeNode, float>();
        dic[idle] = 5;
        dic[patrol] = 10;
        _rouletteRandom = new TreeRandomNode(dic);

        // Machine questions
        var qReachedPatrolPoint = new TreeQuestionNode(QuestionHasReachedPoint, patrol, _rouletteRandom);
        var qCanIdle = new TreeQuestionNode(QuestionIsIdling, qReachedPatrolPoint, idle);
        var qIsAlive = new TreeQuestionNode(() => !gameObject.activeSelf, qCanIdle, dead); // Changes will be needed to check if this object is alive

        _root = qIsAlive;
    }
}
