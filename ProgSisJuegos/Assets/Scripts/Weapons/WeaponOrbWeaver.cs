using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrbWeaver : WeaponBase
{
    [SerializeField] private ProjectileBase _chargedOrb;
    [SerializeField] private float _chargeTime = 1.7f;

    private Transform _spawnTransform;
    private float _currentChargeTime = 0;
    private bool _isCharging = false;

    public WeaponOrbWeaver(WeaponDatabase data)
    {
        InitializeWeapon(data);
    }

    public override void Fire(Transform spawnTransform)
    {
        if (!_isCharging)
        {
            _isCharging = true;
            _currentChargeTime = _chargeTime;

            // If the charge is valid, then nullify it
            if (_chargedOrb != null)
            {
                _chargedOrb.gameObject.SetActive(false);
                _chargedOrb = null;
            }

            _chargedOrb = Pool.CreateProjectile(WeaponType.OrbWeaverCharger);
            _chargedOrb.transform.position = spawnTransform.position;
        }

        _spawnTransform = spawnTransform;

        if (_chargedOrb != null && _spawnTransform != null)
        {
            _chargedOrb.transform.position = _spawnTransform.position;
            _chargedOrb.transform.rotation = _spawnTransform.rotation;
        }
    }

    public override void StopFire()
    {
        _isCharging = false;

        if (_currentChargeTime <= 0)
        {
            ProjectileBase orbProjectile = Pool.CreateProjectile(WeapType);
            orbProjectile.transform.position = _spawnTransform.position;
            orbProjectile.transform.rotation = _spawnTransform.rotation;
            orbProjectile.UpdateStats();
            orbProjectile.UpdateStats(WeaponData.WeapProjectileDamage, WeaponData.WeapProjectileSpeed, WeaponData.WeapProjectileLife);
            UseAmmo();
            _spawnTransform = null;
        }

        if (_chargedOrb != null)
        {
            _chargedOrb.gameObject.SetActive(false);
            _chargedOrb = null;
        }
    }

    public override void Recoil(float deltaTime)
    {
        if (_isCharging && _currentChargeTime > 0) _currentChargeTime -= deltaTime;
        if (!_isCharging) _currentChargeTime = _chargeTime;
    }

    public override void Swapped()
    {
        if (_chargedOrb != null)
        {
            _spawnTransform = null;
            _isCharging = false;
            _currentChargeTime = 0;
            _chargedOrb.gameObject.SetActive(false);
            _chargedOrb = null;
        }
    }
}