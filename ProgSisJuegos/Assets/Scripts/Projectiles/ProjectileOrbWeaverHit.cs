using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrbWeaverHit : ProjectileBase
{
    [SerializeField, Range(1, 10)] private float _ballDamage = 3;
    [SerializeField, Range(1, 10)] private float _ballLife = 3;
    [SerializeField] private int _maxHits = 3;

    private float _ballSpeed = 0;
    private int _hitsLeft;

    public override void UpdateStats()
    {
        _hitsLeft = _maxHits;
        base.UpdateStats(_ballDamage, _ballSpeed, _ballLife);
    }

    public override void ProjectileHit(IDamageable hit)
    {
        hit.AnyDamage(_damage);
        if (_hitsLeft <= 0) _currentLife = 0;
        else _hitsLeft--;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private protected override void OnTriggerEnter(Collider other)
    {
        return;
    }

    private protected override void FixedUpdate()
    {
        return;
    }

}
