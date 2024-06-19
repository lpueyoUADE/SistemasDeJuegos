using UnityEngine;

public class WeaponNone : IWeapon
{
    public WeaponType WeaponType => 0;

    Sprite IWeapon.WeaponIcon => null;

    public void Fire(Transform projectileOutPosition)
    {
        Debug.Log("Empty weapon.");
    }

    public void Recoil(float deltaTime)
    {
        Debug.Log("Empty weapon.");
    }

    public void Swapped()
    {
        Debug.Log("Empty weapon.");
    }

    public void Reset()
    {
        Debug.Log("Empty weapon.");
    }

    public void StopFire()
    {
        Debug.Log("Empty weapon.");
    }
}
