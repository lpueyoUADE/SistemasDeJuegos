using System;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWeapon
{
    private static Dictionary<WeaponType, WeaponDatabase> _weaponsDict = new Dictionary<WeaponType, WeaponDatabase>();
    public static Action<WeaponType, Sprite> OnWeaponCreated;

    // Create weapons with the given list
    public FactoryWeapon(List<WeaponDatabase> weapons)
    {
        foreach (WeaponDatabase weapon in weapons)
            _weaponsDict.Add(weapon.WeapType, weapon);

        Debug.Log($"Factory (WEAPONS): initialized, {_weaponsDict.Count} items.");
    }

    public static IWeapon CreateWeapon(WeaponType type)
    {
        _weaponsDict.TryGetValue(type, out WeaponDatabase data);        

        Debug.Log($"Factory (WEAPONS): Trying to create {type} - value {data}.");
        //UIEvents.OnAddInventoryWeapon.Invoke(type);

        switch (type)
        {
            // Player type
            case WeaponType.BlueRail: return new WeaponBlueRail(data);
            case WeaponType.RedDiamond: return new WeaponRedDiamond(data);

            // Enemy type
            case WeaponType.EnemyBlueRail: return new WeaponEnemyBlueRail(data);

            // Not found
            default: Debug.Log($"Factory (WEAPONS): Weapon of type {type} not found."); return new WeaponNone();
        }
    }
}
