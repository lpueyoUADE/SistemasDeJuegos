using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{
    [SerializeField] protected ShipType _type = ShipType.CannonFoder;
    protected FiniteStateMachine<StatesEnum> _fsm;

    private Action<Vector3, float, ForceMode> ActionMovement;

    protected virtual void Start()
    {
        ActionMovement += Move;
    }

    public virtual void InitializeFSM()
    {
        var patrol = new StatePatrolGeneric<StatesEnum>(ActionMovement);

        _fsm = new FiniteStateMachine<StatesEnum>(patrol);
    }

    
    protected virtual void Update()
    {
        _fsm.OnUpdate();
    }
    
}