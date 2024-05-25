using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{
    // IA
    [SerializeField] protected ShipType _type = ShipType.CannonFoder;
    protected FiniteStateMachine<StatesEnum> _fsm;
    ITreeNode _root;

    private ModifierPatrolPoints _patrolPoints;
    private TreeRandomNode _rouletteRandom;

    protected virtual void Start()
    {
        TryGetComponent(out _patrolPoints);


        InitializeFSM();
        InitializeTree();
    }

    protected virtual void Update()
    {
        _fsm?.OnUpdate();
        _root?.Execute();
    }

    public virtual void InitializeFSM()
    {
        var idle = new StateIdleGeneric<StatesEnum>(this);
        var patrol = new StatePatrolGeneric<StatesEnum>(this, _patrolPoints);
        var dead = new StateDeadGeneric<StatesEnum>();

        idle.AddTransition(StatesEnum.Patrol, patrol);
        idle.AddTransition(StatesEnum.Dead, dead);

        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Dead, dead);

        _fsm = new FiniteStateMachine<StatesEnum>(idle);
    }

    public virtual void InitializeTree()
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
    
    public bool QuestionIsIdling()
    {
        return IsIdling;
    }

    private bool QuestionHasReachedPoint(Vector3 destination, float tolerance = 1)
    {
        if (Vector3.Distance(transform.position, destination) <= tolerance)
            return true;

        return false;
    }

    private bool QuestionHasReachedPoint()
    {
        if (Vector3.Distance(transform.position, _patrolPoints.GetCurrentPatrolPoint()) <= _patrolPoints.Tolerance)
            return true;            

        return false;
    }


    // Testing idle <-> patrol
    Coroutine _idleTimer;
    public bool IsIdling => _idleTimer != null;
    public void StartIdle()
    {
        float time = UnityEngine.Random.Range(1f, 3f);
        _idleTimer = StartCoroutine(Idle(time));
    }

    IEnumerator Idle(float time)
    {
        yield return new WaitForSeconds(time);
        _idleTimer = null;
    }

}