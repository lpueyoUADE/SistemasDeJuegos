using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    None,
    ElCapitan,
    Mosquitoe, 
    CannonFoder,
}

[CreateAssetMenu(fileName = "NewShipData", menuName = "Databases/Ship")]
public class ShipDatabase : ScriptableObject
{
    [SerializeField] protected ShipType _shipType;
    [SerializeField] protected ShipBase _prebab;
    [SerializeField] private int _points;

    [Header("Ship Settings")]
    [SerializeField] private float _life = 10;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField, Range(0, 99)] protected float _damageResistance;
    [SerializeField] protected WeaponType _defaultWeapon;

    [Header("Shield Settings")]
    [SerializeField] private float _shieldDuration = 1;
    [SerializeField] private Color _shieldColor = Color.green;


    public ShipType Type => _shipType;
    public ShipBase Prefab => _prebab;
    public int Points => _points;

    public float Life => _life;
    public float MaxSpeed => _maxSpeed;
    public float Acceleration => _acceleration;
    public float DamageResistance => _damageResistance;
    public WeaponType DefaultWeapon => _defaultWeapon;

}
