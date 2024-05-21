using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    CannonFoder
}

public class EnemyBase : MonoBehaviour
{
    private EnemyType _enemyType;

    public EnemyType EnemyType => _enemyType;
}
