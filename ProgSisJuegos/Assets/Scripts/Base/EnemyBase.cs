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

        Fire();
        Recoil(delta);
        _behaviour?.FSMUpdate(delta);
    }

    
}

