using UnityEngine;

public interface IWeapon
{
    public WeaponType WeaponType { get; }
    void Fire(Vector3 projectileOutPosition);
    void Recoil(float deltaTime);
}
