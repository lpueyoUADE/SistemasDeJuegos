using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRedDiamond : WeaponBase
{
    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponAmmoUpdate(WeapType, _currentAmmo);
        PlayerEvents.OnWeaponFire?.Invoke();

        if (_currentAmmo <= 0) PlayerEvents.OnWeaponAmmoEmpty?.Invoke(this);
    }
}