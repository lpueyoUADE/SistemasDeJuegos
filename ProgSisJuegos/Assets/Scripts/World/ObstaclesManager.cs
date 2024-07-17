using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private Transform _boundaries;

    [Header("Obstacle objects")]
    [SerializeField] private ObstaclesDatabase _obstacleList;
    [SerializeField, Range(0, 250)] private int _maxObstacles = 150;
    [SerializeField, Range(0, 100)] private int _maxActiveObstacles = 60;

    [Header("Obstacle position")]
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();
    [SerializeField] private Vector3 _minRandomOffset;
    [SerializeField] private Vector3 _maxRandomOffset;

    [Header("Obstacle speed")]
    [SerializeField] private float _minRandomSpeed = 3;
    [SerializeField] private float _maxRandomSpeed = 3;

    private List<ObstacleObject> _spawnableList = new List<ObstacleObject>();
    private List<ObstacleObject> _currentObstacles = new List<ObstacleObject>();
    private List<ObstacleObject> _activeObstacles = new List<ObstacleObject>();

    private void Start()
    {
        foreach (ObstacleObject obstacle in _obstacleList.Obstacles) _spawnableList.Add(obstacle);

        for (int i = 0; i < _maxObstacles; i++)
        {
            var spawnedObstacle = Instantiate(_spawnableList[Random.Range(0, _spawnableList.Count)]);

            spawnedObstacle.UpdateBoundaries(_boundaries.position);
            spawnedObstacle.UpdateForward(_boundaries.forward);

            spawnedObstacle.gameObject.SetActive(false);
            spawnedObstacle.OnBoundReached += ObstacleBoundReached;
            _currentObstacles.Add(spawnedObstacle);
        }

        for (int i = 0; i < _maxActiveObstacles; i++) ExecuteObstacle();
    }

    private void ExecuteObstacle()
    {
        if (_activeObstacles.Count >= _maxActiveObstacles) return;

        var obstacle = _currentObstacles[Random.Range(0, _currentObstacles.Count)];

        Vector3 randPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
        randPosition += new Vector3(
            Random.Range(-_minRandomOffset.x, _maxRandomOffset.x),
            Random.Range(-_minRandomOffset.y, _maxRandomOffset.y),
            Random.Range(-_minRandomOffset.z, _maxRandomOffset.z));

        obstacle.transform.position = randPosition;

        _currentObstacles.Remove(obstacle);
        _activeObstacles.Add(obstacle);

        obstacle.gameObject.SetActive(true);
        obstacle.UpdateSpeed(Random.Range(_minRandomSpeed, _maxRandomSpeed));
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _currentObstacles.Count; i++) 
            _currentObstacles[i].OnBoundReached -= ObstacleBoundReached;
    }

    private void ObstacleBoundReached(ObstacleObject obsObject)
    {
        obsObject.gameObject.SetActive(false);

        if (_activeObstacles.Contains(obsObject))
        {
            _currentObstacles.Add(obsObject);
            _activeObstacles.Remove(obsObject);
        }

        ExecuteObstacle();
    }

}
