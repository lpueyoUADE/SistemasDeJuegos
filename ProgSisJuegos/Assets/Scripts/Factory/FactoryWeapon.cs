using System;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWeapon
{
    private static Dictionary<WeaponType, WeaponDatabase> _weaponsDict = new Dictionary<WeaponType, WeaponDatabase>();
    public static Action<WeaponType, Sprite> OnWeaponCreated;

    public static void InitializeFactoryWeapons(List<WeaponDatabase> weapons)
    {
        _weaponsDict.Clear();
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
        switch (type)
        {
            // Player type
            case WeaponType.BlueRail: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponBlueRail();
            case WeaponType.RedDiamond: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponRedDiamond();
            case WeaponType.GreenCrast: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponGreenCrast();
            case WeaponType.HeatTrail: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponHeatTrail();
            case WeaponType.OrbWeaver: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponOrbWeaver();
            case WeaponType.Gamma: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponGamma();
            case WeaponType.HeavyMachine: UIEvents.OnAddInventoryWeapon.Invoke(type, data.WeapInitialAmmoAmount); return new WeaponHeavyMachine();

            // Enemy type
            case WeaponType.EnemyBlueRail: return new WeaponEnemyBlueRail();

            // Not found
            default: Debug.Log($"Factory (WEAPONS): Weapon of type {type} not found."); return new WeaponNone();
        }
    }

    public static WeaponDatabase GetWeaponData(WeaponType type)
    {
        if (_weaponsDict.ContainsKey(type)) return _weaponsDict[type];
        return null;
    }
}
