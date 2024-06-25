using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRedDiamond : WeaponBase
{
    public WeaponRedDiamond(WeaponDatabase data)
    {
        InitializeWeapon(data);
    }

    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponAmmoUpdate(WeapType, _currentAmmo);

        if (_currentAmmo <= 0) PlayerEvents.OnWeaponAmmoEmpty?.Invoke(this);
    }
}
