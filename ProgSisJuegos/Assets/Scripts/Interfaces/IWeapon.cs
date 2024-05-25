using UnityEngine;

public interface IWeapon
{
    public WeaponType WeaponType { get; }
    public Sprite WeaponIcon { get; }
    void Fire(Vector3 projectileOutPosition);
    void Recoil(float deltaTime);
}
