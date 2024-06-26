using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedDiamondBall : ProjectileBase
{
    [SerializeField, Range(1, 10)] private float _ballDamage = 3;
    [SerializeField, Range(0.1f, 10)] private float _ballSpeed = 3;
    [SerializeField, Range(1, 10)] private float _ballLife = 3;
    private IDamageable _lastDamagedEnemy;

    private protected override void Awake()
    {
        base.Awake();
    }

    public override void UpdateStats()
    {
        base.UpdateStats(_ballDamage, _ballSpeed, _ballLife);
    }

    public override void ProjectileHit(IDamageable hit)
    {
        if (hit == _lastDamagedEnemy) return;
        base.ProjectileHit(hit);
    }

    public virtual void LastDamagedEnemy(IDamageable hit)
    {
        _lastDamagedEnemy = hit;
    }


    public override void ProjectileHit(Rigidbody hitRBody)
    {
        hitRBody.TryGetComponent(out IDamageable damageble);
        if (damageble == null || damageble == _lastDamagedEnemy) return;

        base.ProjectileHit(hitRBody);
    }
}
