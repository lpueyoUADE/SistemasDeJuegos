using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGamma : WeaponBase
{
    [SerializeField] private GameObject _trail;

    public WeaponGamma(WeaponDatabase data)
    {
        InitializeWeapon(data);
    }

    public override void Fire(Transform spawnTransform)
    {
        Debug.Log("WeaponGamma fire");

        if (WeaponData.WeapHasInfiniteAmmo || Ammo > 0)
        {
            UseAmmo();
        }
    }
}
