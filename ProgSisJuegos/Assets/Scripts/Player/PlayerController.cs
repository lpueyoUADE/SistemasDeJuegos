using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipBase
{
    [SerializeField] private Animator _anim;
    private Vector3 _movement;
    public event Action<WeaponType> OnWeaponChanged; // needs to be removed
    private float _maxLife;

    protected override void Update()
    {
        float delta = Time.deltaTime;
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.Space)) Fire();
        else StopFire();

        if (Input.GetKeyDown(KeyCode.E)) SwapWeapon(true);
        if (Input.GetKeyDown(KeyCode.Q)) SwapWeapon(false);

        Recoil(delta);
        _anim.SetFloat("dirX", _movement.x);
        _anim.SetFloat("dirY", _movement.z);
    }

    protected override void Start()
    {        
        PlayerEvents.OnPlayerSpawn?.Invoke(this);
        OnWeaponChanged += WeaponSwapTest;
        base.Start();
        SwapWeapon();
        _maxLife = _shipData.Life;
        IncreaseMaxLife(0);

        // Testing
        AddWeapon(WeaponType.RedDiamond);
        AddWeapon(WeaponType.GreenCrast);
        AddWeapon(WeaponType.HeatTrail);
        AddWeapon(WeaponType.OrbWeaver);
        AddWeapon(WeaponType.Gamma);
    }

    public override void AnyDamage(float amount)
    {
        base.AnyDamage(amount);
        PlayerEvents.OnPlayerHPUpdate?.Invoke(ShipCurrentLife, _maxLife);
    }

    public override void Heal(float amount)
    {
        if((_currentLife + amount) < _maxLife)
        {
            _currentLife += amount;            
        }
        else
        {
            _currentLife = _maxLife;
        }

        PlayerEvents.OnPlayerHPUpdate?.Invoke(ShipCurrentLife, _maxLife);
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

    // Testing UI swap indicator
    public void WeaponSwapTest(WeaponType type)
    {
        UIEvents.OnWeaponSwap?.Invoke(type);
    }

    private void FixedUpdate()
    {
        Move(_movement);
    }

    public override void SwapWeapon(bool isNext = true)
    {
        base.SwapWeapon(isNext);
        OnWeaponChanged?.Invoke(ShipCurrentWeapon.WeaponType);
    }


    //For TESTING only

    [ContextMenu("Damage")]
    public void Damage()
    {
        AnyDamage(1);
    }

    [ContextMenu("Heal")]
    public void Repair()
    {
        Heal(2);
    }
}
