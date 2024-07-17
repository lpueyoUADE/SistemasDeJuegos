using System;
using UnityEngine;

public class EnemyBase : ShipBase
{
    protected FSMAIBase _behaviour;
    public event Action OnDisabled;
    private float _destination;

    [SerializeField] float lifeSpan;
    private float currentLifeSpan;

    [SerializeField] private float spawnSpeed;
    private bool positioned = false;


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

        if (ShipIsShielded) 
            UpdateShield(delta);

        if (transform.position.y > 0 && positioned == false) 
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, _destination), spawnSpeed*Time.deltaTime);

        else if(transform.position.y == 0 && positioned == false) 
            positioned = true;

        if (currentLifeSpan >= lifeSpan) 
            Disable();

        else currentLifeSpan += Time.deltaTime;
    }

    public override void OnDeath()
    {
        GameManagerEvents.OnEnemyDestroyed(_shipData.Points);
        OnDisabled?.Invoke();
        base.OnDeath();
    }

    private void Disable()
    {
        currentLifeSpan = 0;
        positioned = false;
        GameManagerEvents.OnEnemyDestroyed(0);
        OnDisabled?.Invoke();
        base.OnDeath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().AnyDamage(ShipData.CollisionDamage);
            Disable();
        }
    }

}