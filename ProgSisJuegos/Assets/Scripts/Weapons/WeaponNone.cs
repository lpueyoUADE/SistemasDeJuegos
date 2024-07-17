using UnityEngine;

public class WeaponNone : IWeapon
{
    public WeaponType WeaponType => 0;

    Sprite IWeapon.WeaponIcon => null;

    public void UpdateAmmo(int ammoAmount = 0, bool replaceAmount = false)
    {
        Debug.LogError("Empty weapon.");
    }

    public void Fire(Transform projectileOutPosition)
    {
        Debug.LogError("Empty weapon.");
    }

    public void Recoil(float deltaTime)
    {
        Debug.LogError("Empty weapon.");
    }

    public void Swapped()
    {
        Debug.LogError("Empty weapon.");
    }

    public void Reset()
    {
        Debug.LogError("Empty weapon.");
    }

    public void StopFire()
    {
        Debug.LogError("Empty weapon.");
    }

    public virtual void PlaySound(AudioSource source, AudioClip clip, float volume = 1)
    {
        Debug.LogError("Empty weapon.");
    }

    public void InitializeWeapon(WeaponDatabase data)
    {
        Debug.LogError("Empty weapon.");
    }
}
