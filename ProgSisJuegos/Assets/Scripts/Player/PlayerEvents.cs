using System;
using UnityEngine;

public static class PlayerEvents
{
    public static Action<PlayerController> OnPlayerSpawn;
    public static Action<float, float> OnPlayerHPUpdate;
    public static Action OnPlayerDeath;
    public static Action<float> OnPlayerMaxLifeUpdate;
    public static Action<float> OnShielded;
    public static Action OnShieldEnd;
    public static Action OnPlayerDamaged;

    public static Action<ItemDatabase> OnItemGrab;
    public static Action<WeaponType> OnWeaponSwap;
    public static Action OnWeaponFire;
    public static Action<WeaponType, int> OnWeaponAmmoUpdate;
    public static Action<AudioClip, float> OnWeaponPlaySound;
    public static Action<IWeapon> OnWeaponAmmoEmpty;
}
