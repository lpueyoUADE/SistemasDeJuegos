using System;
using UnityEngine;

public class EnemyBase : ShipBase
{
    protected FSMAIBase _behaviour;
    public event Action OnDisabled;
    private float _destination;

    protected override void Start()
    {
        TryGetComponent(out _behaviour);
        InitializeWeapons();
        _destination = transform.position.z - 37;
    }

    protected override void Update()
    {
        float delta = Time.deltaTime;
        Recoil(delta);
        _behaviour?.FSMUpdate(delta);
        if (ShipIsShielded) UpdateShield(delta);
    }

    public override void OnDeath()
    {
        GameManagerEvents.OnEnemyDestroyed(_shipData.Points);
        OnDisabled?.Invoke();
        base.OnDeath();
    }
    
}