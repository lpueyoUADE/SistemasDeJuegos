using UnityEngine;

public class EnemyBase : ShipBase
{
    protected FSMAIBase _behaviour;

    protected override void Start()
    {
        TryGetComponent(out _behaviour);
        InitializeWeapons();
    }

    protected override void Update()
    {
        float delta = Time.deltaTime;
        if (ShipIsShielded) UpdateShield(delta);

        //Fire();
        //Recoil(delta);
        //_behaviour?.FSMUpdate(delta);
    }

    public override void OnDeath()
    {
        GameManagerEvents.OnEnemyDestroyed(_shipData.Points);
        base.OnDeath();
    }
}