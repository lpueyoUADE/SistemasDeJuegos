using UnityEngine;

public interface IWeapon
{
    public WeaponType WeaponType { get; }
    public Sprite WeaponIcon { get; }
    void Fire(Transform projectileOutTransform);
    void Recoil(float deltaTime);
    void Reset();
}
