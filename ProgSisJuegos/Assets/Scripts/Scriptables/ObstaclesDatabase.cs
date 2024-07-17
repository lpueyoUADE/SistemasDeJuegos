using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstaclesData", menuName = "Databases/Obstacles")]
public class ObstaclesDatabase : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] private List<ObstacleObject> _obstacles = new List<ObstacleObject>();

    [Header("Amount settings")]
    [SerializeField, Range(0, 250)] private int _maxObstacles = 150;
    [SerializeField, Range(0, 100)] private int _maxActiveObstacles = 60;

    [Header("Offset settings")]
    [SerializeField] private Vector3 _minRandomOffset;
    [SerializeField] private Vector3 _maxRandomOffset;

    [Header("Speed settings")]
    [SerializeField] private float _minRandomSpeed = 3;
    [SerializeField] private float _maxRandomSpeed = 3;

    [SerializeField] private float _minRandomMaxSpeed = 3;
    [SerializeField] private float _maxRandomMaxSpeed = 3;

    public List<ObstacleObject> Obstacles => _obstacles;

    public float ObstacleMaxAmount => _maxObstacles;
    public float ObstacleMaxActiveAmount => _maxActiveObstacles;

    public Vector3 ObstacleMinRandomOffset => _minRandomOffset;
    public Vector3 ObstacleMaxRandomOffset => _maxRandomOffset;

    public float ObstacleMinRandomSpeed => _minRandomSpeed;
    public float ObstacleMaxRandomSpeed => _maxRandomSpeed;

    public float ObstacleMinRandomMaxSpeed => _minRandomMaxSpeed;
    public float ObstacleMaxRandomMaxSpeed => _maxRandomMaxSpeed;
}
