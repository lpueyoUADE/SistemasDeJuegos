using UnityEngine;

public class WeaponBase : IWeapon
{
    private WeaponDatabase _weaponData;
    protected int _currentAmmo;
    protected float _currentRecoil;

    public int Ammo => _currentAmmo;
    public float CurrentRecoil => _currentRecoil;

    WeaponType IWeapon.WeaponType { get => WeapType; }
    Sprite IWeapon.WeaponIcon { get => WeaponData.WeapIcon; }
    public WeaponDatabase WeaponData => _weaponData;
    public WeaponType WeapType => WeaponData.WeapType;

    public void InitializeWeapon(WeaponDatabase data)
    {
        _weaponData = data;
        _currentAmmo = _weaponData.WeapInitialAmmoAmount;
    }

    public virtual void Fire(Transform spawnTransform)
    {
        if ((!WeaponData.WeapHasInfiniteAmmo || _currentAmmo <= 0) && _currentRecoil > 0) return;

        ProjectileBase projectile = Pool.CreateProjectile(WeapType);
        projectile.transform.rotation = spawnTransform.rotation;
        projectile.transform.position = spawnTransform.position;
        projectile.UpdateStats(WeaponData.WeapProjectileDamage, WeaponData.WeapProjectileSpeed, WeaponData.WeapProjectileLife);
        _currentRecoil = WeaponData.WeapRecoil;
        UseAmmo();
    }

    public virtual void StopFire()
    {

    }

    public virtual void Recoil(float deltaTime)
    {
        if (_currentRecoil > 0) _currentRecoil -= deltaTime;
    }

    public virtual void UseAmmo()
    {
        if (!WeaponData.WeapHasInfiniteAmmo) _currentAmmo -= 1;
    }

    public virtual void Swapped()
    {

    }

    public virtual void Reset()
    {
        _currentAmmo = _weaponData.WeapInitialAmmoAmount;
        _currentRecoil = 0;
    }
}
