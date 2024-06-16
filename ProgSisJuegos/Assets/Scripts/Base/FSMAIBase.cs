using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IAQuestions
{
    qIsIdling, qHasReachedWaypoint
}

public class FSMAIBase : MonoBehaviour, IFSMBehaviour
{
    [SerializeField] private IABehaviourDatabase _behaviourData;

    // Basics
    protected FiniteStateMachine<StatesEnum> _fsm;
    protected TreeRandomNode _rouletteRandom;
    protected ITreeNode _root;

    // Values
    Coroutine _tIdle;

    // References
    protected EnemyBase _controller;
    protected ModifierPatrolPoints _patrolPoints;

    // Public values
    public ModifierPatrolPoints PatrolPoints => _patrolPoints;
    public IABehaviourDatabase BehaviourData => _behaviourData;

    private void Awake()
    {
        TryGetComponent(out _controller);
        TryGetComponent(out _patrolPoints);
    }

    protected virtual void Start()
    {
        InitializeFSM();
        InitializeTree();
    }
    
    public virtual void FSMUpdate(float deltaTime)
    {
        _fsm?.OnUpdate();
        _root?.Execute();
    }
    
    public virtual void InitializeFSM()
    {
        // Create states and transitions
        var idle = new StateIdleGeneric<StatesEnum>(_controller, this);
        var dead = new StateDeadGeneric<StatesEnum>();

        idle.AddTransition(StatesEnum.Dead, dead);

        // Set default state and default state
        _fsm = new FiniteStateMachine<StatesEnum>(idle);
    }
    
    public virtual void InitializeTree()
    {
        // Create actions
        var idle = new TreeActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var dead = new TreeActionNode(() => _fsm.Transition(StatesEnum.Dead));

        // Machine questions
        // Question => if false do, if true do
        var qIsAlive = new TreeQuestionNode(() => !gameObject.activeSelf, idle, dead); // Changes may be needed to check if this object is alive
        _root = qIsAlive;
    }

    // Timers
    public virtual void ActionStartIdle(float time)
    {
        _tIdle = StartCoroutine(TimerIdle(time));
    }
    IEnumerator TimerIdle(float time)
    {
        yield return new WaitForSeconds(time);
        _tIdle = null;
    }

    // Func questions
    public virtual bool QuestionIsIdling()
    {
        return _tIdle != null;
    }
    public virtual bool QuestionHasReachedPoint(Vector3 destination, float tolerance = 1)
    {
        if (Vector3.Distance(transform.position, destination) <= tolerance)
            return true;

        return false;
    }
    public virtual bool QuestionHasReachedPoint()
    {
        if (Vector3.Distance(transform.position, _patrolPoints.GetCurrentPatrolPoint()) <= _patrolPoints.Tolerance)
            return true;

        return false;
    }
}
