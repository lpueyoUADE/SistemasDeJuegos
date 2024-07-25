using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlueRail : WeaponBase
{
    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponFire?.Invoke();
    }
}