using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyWaveData", menuName = "Databases/Enemy Wave")]
public class EnemyWaveDataBase : ScriptableObject
{
    [SerializeField] private EnemyBase[] _enemies;
    public ShipBase[] Enemies => _enemies;


}
