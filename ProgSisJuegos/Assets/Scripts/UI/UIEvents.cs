using System;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public UIEvents() { }

    // UI Actions & Events
    public static Action<List<WeaponDatabase>> OnAllWeaponsInitialize;
    public static Action<WeaponType> OnAddInventoryWeapon;
    public static Action<WeaponType> OnRemoveInventoryWeapon;
    public static Action<WeaponType> OnWeaponSwap;

    public static Action OnPlayerSpawn;
    public static Action<float> OnPlayerHPUpdate;
    public static Action<float> OnPlayerMaxLifeUpdate;
    public static Action OnPlayerDeath;

    public static Action<float> OnScoreUpdate;
}
