using UnityEngine;

public class WeaponBase : IWeapon
{
    private WeaponDatabase _weaponData;
    private int _currentAmmo;
    private float _currentRecoil;

    public int Ammo => _currentAmmo;
    public float CurrentRecoil => _currentRecoil;

    WeaponType IWeapon.WeaponType { get => WeapType; }
    Sprite IWeapon.WeaponIcon { get => WeaponData.Icon; }
    public WeaponDatabase WeaponData => _weaponData;
    public WeaponType WeapType => WeaponData.WeapType;

    public void InitializeWeapon(WeaponDatabase data)
    {
        _weaponData = data;
    }

    public virtual void Fire(Vector3 spawnPosition)
    {
        if ((WeaponData.WeapHasInfiniteAmmo || _currentAmmo > 0) && _currentRecoil <= 0)
        {
            ProjectileBase projectile = Pool.CreateProjectile(WeapType);
            projectile.UpdateStats(WeaponData.WeapDamage, WeaponData.WeapProjectileSpeed);
            projectile.transform.position = spawnPosition;
            _currentRecoil = WeaponData.WeapRecoil;
        }
        else { Debug.Log("No ammo"); }
    }

    public virtual void Recoil(float deltaTime)
    {
        if (_currentRecoil > 0) _currentRecoil -= deltaTime;
    }


}
