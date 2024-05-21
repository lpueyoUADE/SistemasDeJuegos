using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Databases/Enemy Ship")]
public class EnemyDatabase : MonoBehaviour
{
    [Header("Ship Settings")]
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 99)] private float _damageResistance;
    [SerializeField] private WeaponType _defaultWeapon;
    [SerializeField] private int _scoreReward;

    public float Speed => _speed;
    public float DamageResistance => _damageResistance;
    public WeaponType DefaultWeapon => _defaultWeapon;
    public int Score => _scoreReward;
}
