using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemyGreenCrast : WeaponBase
{
    public override void Fire(Transform spawnTransform)
    {
        if (_currentRecoil > 0) return;

        if (SFX1 != null)
        {
            var firesfx = Pool.CreateUniversalObject(UniversalPoolObjectType.Audio);
            firesfx.transform.position = spawnTransform.position;
            firesfx.UpdateAudioAndPlay(SFX1);
        }

        ProjectileBase projectile = Pool.CreateProjectile(WeapType);
        projectile.transform.rotation = spawnTransform.rotation;
        projectile.transform.position = spawnTransform.position;
        projectile.UpdateStats(WeaponData.WeapProjectileDamage, WeaponData.WeapProjectileSpeed, WeaponData.WeapProjectileLife);
        _currentRecoil = WeaponData.WeapRecoil;
    }
}