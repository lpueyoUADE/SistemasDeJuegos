using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    BlueRail, RedDiamond, RedDiamondBall, GreenCrast, HeatTrail, OrbWeaver, OrbWeaverCharger, OrbWeaverHit, Gamma,
    EnemyBlueRail
}

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Databases/Weapon")]
public class WeaponDatabase : ScriptableObject
{
    [Header("Weapon Settings")]
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private WeaponType _type = WeaponType.BlueRail;
    [SerializeField, Range(0, 5)] private float _recoil = 1;
    [SerializeField] private bool _hasInfiniteAmmo = false;
    [SerializeField] private int _defaultAmmoAmount = 1;

    [Header("Projectile Settings")]
    [SerializeField] private ProjectileBase _projectilePrefab;
    [SerializeField, Range(0, 100)] private float _projectileDamage = 1;
    [SerializeField, Range(0, 20)] private float _projectileSpeed = 3;
    [SerializeField, Range(0, 20)] private float _projectileLife = 5;

    [Header("Sound settings")]
    [SerializeField] private AudioClip _sfx1;
    [SerializeField] private AudioClip _sfx2;
    [SerializeField] private AudioClip _sfx3;
    [SerializeField] private AudioClip _sfx4;
    [SerializeField] private AudioClip _sfx5;

    public Sprite WeapIcon => _weaponIcon;
    public WeaponType WeapType => _type;    
    public float WeapRecoil => _recoil;
    public bool WeapHasInfiniteAmmo => _hasInfiniteAmmo;
    public int WeapInitialAmmoAmount => _defaultAmmoAmount;

    public ProjectileBase WeapProjectilePrefab => _projectilePrefab;
    public float WeapProjectileDamage => _projectileDamage;
    public float WeapProjectileSpeed => _projectileSpeed;
    public float WeapProjectileLife => _projectileLife;

    public AudioClip WeapSFX1 => _sfx1;
    public AudioClip WeapSFX2 => _sfx2;
    public AudioClip WeapSFX3 => _sfx3;
    public AudioClip WeapSFX4 => _sfx4;
    public AudioClip WeapSFX5 => _sfx5;


}
