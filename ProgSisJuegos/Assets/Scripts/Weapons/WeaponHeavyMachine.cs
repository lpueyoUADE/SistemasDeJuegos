using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeavyMachine : WeaponBase
{
    public override void UseAmmo()
    {
        base.UseAmmo();
        PlayerEvents.OnWeaponFire?.Invoke();
    }
}