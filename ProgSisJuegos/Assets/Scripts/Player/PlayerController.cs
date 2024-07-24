using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipBase
{
    [SerializeField] private Animator _anim;
    private Vector3 _movement;
    private float _maxLife;

    protected override void Start()
    {
        PlayerEvents.OnItemGrab += OnItemGrab;
        PlayerEvents.OnPlayerSpawn?.Invoke(this);
        PlayerEvents.OnWeaponPlaySound += PlayWeaponSound;
        PlayerEvents.OnWeaponAmmoEmpty += RemoveWeapon;

        base.Start();
        SwapWeapon();
        _maxLife = _shipData.Life;
    }

    void OnDestroy()
    {
        PlayerEvents.OnItemGrab -= OnItemGrab;
        PlayerEvents.OnWeaponPlaySound -= PlayWeaponSound;
        PlayerEvents.OnWeaponAmmoEmpty -= RemoveWeapon;
    }

    protected override void Update()
    {
        float delta = Time.deltaTime;
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.Space)) Fire();
        else StopFire();

        if (Input.GetKeyDown(KeyCode.E)) SwapWeapon(true);
        if (Input.GetKeyDown(KeyCode.Q)) SwapWeapon(false);

        UpdateShield(delta);
        Recoil(delta);
        _anim.SetFloat("dirX", _movement.x);
        _anim.SetFloat("dirY", _movement.z);
    }

    private void FixedUpdate()
    {
        Move(_movement);
    }

    private void OnItemGrab(ItemDatabase itemData)
    {
        if (itemData == null) return;

        // Grabbing consumable
        if (itemData.ItemType == ItemType.Consumable)
        {
            ItemConsumableDatabase consumable = (ItemConsumableDatabase)itemData;

            switch (consumable.ItemConsumableType)
            {
                case ConsumableType.None: return;
                case ConsumableType.Shield: Shield(itemData.ItemValue, ShipShieldColor); Debug.Log($"Shielding for {itemData.ItemValue}."); break;
                case ConsumableType.Repair: ShipRepair(itemData.ItemValue); Debug.Log($"Repairing for {itemData.ItemValue}."); break;
                case ConsumableType.ExtraLife: Debug.Log($"{itemData.ItemValue} extra life/s."); break;
            }
        }

        // Grabbing item weapon
        if (itemData.ItemType == ItemType.Weapon)
        {
            ItemWeaponDatabase weapon = (ItemWeaponDatabase)itemData;

            if (weapon.ItemWeaponType == WeaponType.None) return;
            AddWeapon(weapon.ItemWeaponType, (int)itemData.ItemValue);
        }
    }

    public override void AnyDamage(float amount)
    {
        base.AnyDamage(amount);
        PlayerEvents.OnPlayerHPUpdate?.Invoke(ShipCurrentLife, ShipData.Life);
        PlayerEvents.OnPlayerDamaged?.Invoke();
    }

    public override void ShipRepair(float amount)
    {
        base.ShipRepair(amount);
        PlayerEvents.OnPlayerHPUpdate?.Invoke(ShipCurrentLife, ShipData.Life);
    }

    private void IncreaseMaxLife(float amount)
    {
        _maxLife += amount;
        PlayerEvents.OnPlayerHPUpdate?.Invoke(ShipCurrentLife, _maxLife);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        PlayerEvents.OnPlayerDeath?.Invoke();
    }

    public override void SwapWeapon(bool isNext = true)
    {
        base.SwapWeapon(!isNext);
        UIEvents.OnWeaponSwapLeftRight?.Invoke(!isNext);
        PlayerEvents.OnWeaponSwap?.Invoke(ShipCurrentWeapon.WeaponType);
    }

    public override void SwapWeapon(WeaponType type)
    {
        base.SwapWeapon(type);
        UIEvents.OnWeaponSwap?.Invoke(ShipCurrentWeapon.WeaponType);
    }

    public override void RemoveWeapon(IWeapon type)
    {
        UIEvents.OnRemoveInventoryWeapon?.Invoke(ShipCurrentWeapon.WeaponType);
        base.RemoveWeapon(type);
        UIEvents.OnWeaponSwap?.Invoke(ShipCurrentWeapon.WeaponType);
    }

    public override void Shield(float duration, Color color)
    {
        base.Shield(duration, color);
        PlayerEvents.OnShielded?.Invoke(duration);
    }

    public override void UpdateShield(float delta)
    {
        if (ShipIsShielded)
        {
            _shieldTimeLeft -= delta;
            if (_shieldScript != null) _shieldScript.UpdateShieldTime(_shieldTimeLeft);
        }

        if (!ShipIsShielded && _shieldObject.gameObject.activeSelf)
        {
            _shieldObject.gameObject.SetActive(false);
            PlayerEvents.OnShieldEnd?.Invoke();
        }
    }

    private void PlayWeaponSound(AudioClip clip, float volume)
    {
        Audio.PlayOneShot(clip, volume);
    }

#if UNITY_EDITOR
    [ContextMenu("Heal")]
    public void Repair()
    {
        ShipRepair(2);
    }
#endif
}
