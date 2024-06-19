using System;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWeapon
{
    private static Dictionary<WeaponType, WeaponDatabase> _weaponsDict = new Dictionary<WeaponType, WeaponDatabase>();
    public static Action<WeaponType, Sprite> OnWeaponCreated;

    public static void InitializeFactoryWeapons(List<WeaponDatabase> weapons)
    {
        string message = "Factory Weapons: \n";
        foreach (WeaponDatabase weapon in weapons)
        {
            _weaponsDict.Add(weapon.WeapType, weapon);
            message += $"{weapon.WeapType}, ";
        }

        Debug.Log($"{message} Initialized {_weaponsDict.Count} items.");
    }

    public static IWeapon CreateWeapon(WeaponType type)
    {
        _weaponsDict.TryGetValue(type, out WeaponDatabase data);        

        //Debug.Log($"Factory (WEAPONS): Trying to create {type} - value {data}.");
        UIEvents.OnAddInventoryWeapon.Invoke(type);

        switch (type)
        {
            // Player type
            case WeaponType.BlueRail: return new WeaponBlueRail(data);
            case WeaponType.RedDiamond: return new WeaponRedDiamond(data);
            case WeaponType.GreenCrast: return new WeaponGreenCrast(data);
            case WeaponType.HeatTrail: return new WeaponHeatTrail(data);
            case WeaponType.OrbWeaver: return new WeaponOrbWeaver(data);
            case WeaponType.Gamma: return new WeaponGamma(data);

            // Enemy type
            case WeaponType.EnemyBlueRail: return new WeaponEnemyBlueRail(data);

            // Not found
            default: Debug.Log($"Factory (WEAPONS): Weapon of type {type} not found."); return new WeaponNone();
        }
    }
}
