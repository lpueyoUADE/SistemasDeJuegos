using UnityEngine;

public class WeaponNone : IWeapon
{
    public WeaponType WeaponType => throw new System.NotImplementedException();

    Sprite IWeapon.WeaponIcon => throw new System.NotImplementedException();

    public void Fire(Transform projectileOutPosition)
    {
        Debug.Log("Empty weapon.");
    }

    public void Recoil(float deltaTime)
    {
        Debug.Log("Empty weapon.");
    }

    public void Reset()
    {
        Debug.Log("Empty weapon.");
    }
}
