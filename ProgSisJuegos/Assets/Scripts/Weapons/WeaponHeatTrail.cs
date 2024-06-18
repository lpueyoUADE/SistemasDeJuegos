using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHeatTrail : WeaponBase
{
    public WeaponHeatTrail(WeaponDatabase data)
    {
        InitializeWeapon(data);
    }

    public override void Fire(Transform spawnTransform)
    {
        Debug.Log("HeatTrail fire");

        if (WeaponData.WeapHasInfiniteAmmo || Ammo > 0)
        {
            UseAmmo();
        }
    }
}
