using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGamma : WeaponBase
{
    [SerializeField] private ProjectileBase _trail;

    public override void Fire(Transform spawnTransform)
    {
        if ((!WeaponData.WeapHasInfiniteAmmo && _currentAmmo <= 0) || _currentRecoil > 0) return;
        if (_trail == null) _trail = Pool.CreateProjectile(WeapType);

        _trail.transform.rotation = spawnTransform.rotation;
        _trail.transform.position = spawnTransform.position;
        _trail.transform.localScale = new Vector3(_trail.transform.localScale.x, _trail.transform.localScale.y, 800);
        UseAmmo();
    }

    public override void StopFire()
    {
        if (_trail != null)
        {
            _trail.gameObject.SetActive(false);
            _trail = null;
        }
    }

    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponAmmoUpdate(WeapType, _currentAmmo);

        if (_currentAmmo <= 0)
        {
            StopFire();
            PlayerEvents.OnWeaponAmmoEmpty?.Invoke(this);
        }
    }

    public override void Swapped()
    {
        if (_trail != null)
        {
            _trail.gameObject.SetActive(false);
            _trail = null;
        }
    }
}
