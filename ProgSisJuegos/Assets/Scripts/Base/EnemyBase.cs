using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{
    protected FSMAIBase _behaviour;

    protected virtual void Start()
    {
        TryGetComponent(out _behaviour);
    }

    protected override void Update()
    {
        float delta = Time.deltaTime;

        ShipCurrentWeapon?.Recoil(delta);
        _behaviour?.FSMUpdate(delta);
    }
}