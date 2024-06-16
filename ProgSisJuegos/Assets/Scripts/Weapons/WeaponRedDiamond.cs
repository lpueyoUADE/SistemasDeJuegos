using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRedDiamond : WeaponBase
{
    public WeaponRedDiamond(WeaponDatabase data)
    {
        InitializeWeapon(data);
    }

    public void Fire()
    {
        Debug.Log("red caca " + WeaponData.WeapHasInfiniteAmmo);
    }
}
