using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipBase
{
    private Vector3 _movement;
    public event Action<int> OnHpChanged;
    public event Action<WeaponType> OnWeaponChanged;

    protected override void Update()
    {
        float delta = Time.deltaTime;
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.Space))
            Fire();

        if (Input.GetKeyDown(KeyCode.E))
            SwapWeapon(true);        
        
        if (Input.GetKeyDown(KeyCode.Q))
            SwapWeapon(false);

        Recoil(delta);
    }

    protected override void Start()
    {
        OnWeaponChanged += WeaponSwapTest;
        base.Start();
        SwapWeapon();        

        // Testing
        AddWeapon(WeaponType.RedDiamond);
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
}
