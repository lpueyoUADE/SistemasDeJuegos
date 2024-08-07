using System;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public static Action<AudioClip, float> OnPlayUISound;
    public static Action<bool> OnGamePaused;

    public static Action<List<WeaponDatabase>> OnAllWeaponsInitialize;
    public static Action<WeaponType, int> OnAddInventoryWeapon;
    public static Action<WeaponType> OnRemoveInventoryWeapon;
    public static Action<WeaponType> OnWeaponSwap;
    public static Action<bool> OnWeaponSwapLeftRight;

    public static Action OnPlayerSpawn;
    public static Action<float, float> OnPlayerHPUpdate;
    public static Action<bool> OnGameEnded;


    public static Action<float> OnScoreUpdate;
}
