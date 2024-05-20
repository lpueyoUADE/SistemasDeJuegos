using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipData", menuName = "Databases/Ship")]
public class ShipDatabase : ScriptableObject
{
    [Header("Ship Settings")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField, Range(0, 99)] private float _damageResistance;
    [SerializeField] private WeaponType _defaultWeapon;

    public float MaxSpeed => _maxSpeed;
    public float Acceleration => _acceleration;
    public float DamageResistance => _damageResistance;
    public WeaponType DefaultWeapon => _defaultWeapon;

    [Header("Shield Settings")]
    [SerializeField] private float _shieldDuration = 1;
    [SerializeField] private Color _shieldColor = Color.green;

}
