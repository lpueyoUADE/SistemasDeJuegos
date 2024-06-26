using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    None,
    ElCapitan,
    Sonic,
    SkullFlower,
    Mosquitoe,
    Slider,
    Tremor,
    CannonFoder,
}

[CreateAssetMenu(fileName = "NewShipData", menuName = "Databases/Ship")]
public class ShipDatabase : ScriptableObject
{
    [SerializeField] protected ShipType _shipType;
    [SerializeField] protected ShipBase _prebab;
    [SerializeField] private int _points;

    [Header("Showcase")]
    [SerializeField] protected string _shipNameToUser;
    [SerializeField] protected GameObject _showcaseObject;

    [Header("Settings")]
    [SerializeField] private float _life = 10;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField, Range(0, 99)] protected float _damageResistance;
    [SerializeField] protected WeaponType _defaultWeapon;

    [Header("Shield")]
    [SerializeField, Range(0, 5)] private float _shieldDuration = 1;
    [SerializeField] private Color _shieldColor = Color.green;


    public ShipType Type => _shipType;
    public string Name => _shipNameToUser;
    public ShipBase Prefab => _prebab;
    public GameObject ShowcasePrefab => _showcaseObject;
    public int Points => _points;

    public float Life => _life;
    public float MaxSpeed => _maxSpeed;
    public float Acceleration => _acceleration;
    public float DamageResistance => _damageResistance;
    public WeaponType DefaultWeapon => _defaultWeapon;

    public float ShieldDuration => _shieldDuration;
    public Color ShieldColor => _shieldColor;

}
