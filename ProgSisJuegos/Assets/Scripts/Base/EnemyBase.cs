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
        if (ShipIsShielded) UpdateShield(delta);

        if (transform.position.y > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, _destination), 0.5f);
        }
        //Fire();
        //Recoil(delta);
        //_behaviour?.FSMUpdate(delta);
    }

    public override void AnyDamage(float amount)
    {
        _currentLife -= amount;
        if (_currentLife <= 0)
        {
            OnDeath();
        }
    }

    [ContextMenu("Destroy")]
    public override void OnDeath()
    {
        GameManagerEvents.OnEnemyDestroyed(_shipData.Points);
        base.OnDeath();
    }
    
}