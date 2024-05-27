using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    BlueRail, RedDiamond, RedDiamondBall, GreenCrast, HeatTrail, OrbWeaver, Gamma,
    EnemyBlueRail
}

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Databases/Weapon")]
public class WeaponDatabase : ScriptableObject
{
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private WeaponType _type = WeaponType.BlueRail;
    [SerializeField, Range(0, 100)] private float _damage = 1;
    [SerializeField, Range(0, 5)] private float _recoil = 1;
    [SerializeField] private bool _hasInfiniteAmmo = false;
    [SerializeField] private int _defaultAmmoAmount = 1;

    [SerializeField, Range(0, 20)] private float _projectileSpeed = 3;

    public Sprite WeapIcon => _weaponIcon;
    public WeaponType WeapType => _type;
    public float WeapDamage => _damage;
    public float WeapRecoil => _recoil;
    public bool WeapHasInfiniteAmmo => _hasInfiniteAmmo;
    public int WeapInitialAmmoAmount => _defaultAmmoAmount;
    public float WeapProjectileSpeed => _projectileSpeed;
}
