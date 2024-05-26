using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipBase
{
    private Vector3 _movement;
    public event Action<int> OnHpChanged;
    public event Action OnWeaponChanged;

    protected override void Update()
    {
        float delta = Time.deltaTime;
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.Space))
            Fire();

        if (ShipCurrentWeapon != null) ShipCurrentWeapon.Recoil(delta);
    }

    private void FixedUpdate()
    {
        Move(_movement);
    }
}
