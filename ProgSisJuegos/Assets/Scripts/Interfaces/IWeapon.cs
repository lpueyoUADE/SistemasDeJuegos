using UnityEngine;

public interface IWeapon
{ 
    public WeaponType WeaponType { get; }
    public Sprite WeaponIcon { get; }
    void UpdateAmmo(int ammoAmount = 0, bool replaceAmount = false);
    void Fire(Transform projectileOutTransform);
    void StopFire();
    void Recoil(float deltaTime);
    void Swapped();
    void Reset();
}
