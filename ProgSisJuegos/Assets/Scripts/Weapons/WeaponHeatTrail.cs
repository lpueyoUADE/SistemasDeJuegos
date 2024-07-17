using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeatTrail : WeaponBase
{
    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponAmmoUpdate(WeapType, _currentAmmo);

        if (_currentAmmo <= 0) PlayerEvents.OnWeaponAmmoEmpty?.Invoke(this);
    }
}
