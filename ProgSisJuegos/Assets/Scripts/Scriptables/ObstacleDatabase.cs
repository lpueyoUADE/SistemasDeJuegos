using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Databases/Obstacle")]
public class ObstacleDatabase : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;

    public float Speed { get { return speed; }}
    public float Lifetime { get { return lifetime; }}
    public float Damage { get { return damage; }}
}
