using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedDiamondBall : ProjectileBase
{
    private IDamageable _lastDamagedEnemy;

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
