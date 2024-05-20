using UnityEngine;

public class WeaponNone : IWeapon
{
    public WeaponType WeaponType => throw new System.NotImplementedException();

    public void Fire(Vector3 projectileOutPosition)
    {
        Debug.Log("Empty weapon.");
    }

    public void Recoil(float deltaTime)
    {
        Debug.Log("Empty weapon.");
    }
}
