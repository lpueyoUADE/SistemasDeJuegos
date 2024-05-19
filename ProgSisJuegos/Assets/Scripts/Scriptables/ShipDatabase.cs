using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipData", menuName = "Databases/Ship")]
public class ShipDatabase : ScriptableObject
{
    [Header("General Ship Settings")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
}
