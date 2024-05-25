using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWeapon
{
    private static Dictionary<WeaponType, WeaponDatabase> _weaponsDict = new Dictionary<WeaponType, WeaponDatabase>();

    // Create weapons with the given list
    public FactoryWeapon(List<WeaponDatabase> weapons)
    {
        foreach (WeaponDatabase weapon in weapons)
            _weaponsDict.Add(weapon.WeapType, weapon);

        Debug.Log($"Factory: Weapons initialized, {_weaponsDict.Count} items.");
    }

    public static IWeapon CreateWeapon(WeaponType type)
    {
        _weaponsDict.TryGetValue(type, out WeaponDatabase data);
        Debug.Log($"Factory (WEAPONS): Trying to create {type} - value {data}.");

        switch (type)
        {
            case WeaponType.BlueRail: return new WeaponBlueRail(data);
            case WeaponType.RedDiamond: return new WeaponBlueRail(data);
            default: return new WeaponNone();
        }
    }
}
