using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstaclesData", menuName = "Databases/Obstacles")]
public class ObstaclesDatabase : ScriptableObject
{
    [SerializeField] private List<ObstacleObject> _obstacles = new List<ObstacleObject>();

    public List<ObstacleObject> Obstacles => _obstacles;
}
