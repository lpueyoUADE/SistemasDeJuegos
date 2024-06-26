using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedDiamond : ProjectileBase
{
    public override void ProjectileHit(IDamageable hit)
    {
        Vector3 position = transform.position;
        Vector3 right = transform.right;

        ProjectileRedDiamondBall ballLeft = (ProjectileRedDiamondBall)Pool.CreateProjectile(WeaponType.RedDiamondBall);
        //ballLeft.UpdateStats();
        ballLeft.LastDamagedEnemy(hit);
        ballLeft.transform.position = position;
        ballLeft.transform.forward = -right;

        ProjectileRedDiamondBall ballRight = (ProjectileRedDiamondBall)Pool.CreateProjectile(WeaponType.RedDiamondBall);
        //ballRight.UpdateStats();
        ballRight.LastDamagedEnemy(hit);
        ballRight.transform.position = position;
        ballRight.transform.forward = right;

        base.ProjectileHit(hit);
    }
}
