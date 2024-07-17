using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private Transform _boundaries;
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();

    private List<ObstacleObject> _spawnableList = new List<ObstacleObject>();
    private List<ObstacleObject> _currentObstacles = new List<ObstacleObject>();
    private List<ObstacleObject> _activeObstacles = new List<ObstacleObject>();

    private void Start()
    {
        foreach (ObstacleObject obstacle in ScenarioPersistentData.SceneData.SceneObstacles.Obstacles) _spawnableList.Add(obstacle);

        for (int i = 0; i < ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxAmount; i++)
        {
            var spawnedObstacle = Instantiate(_spawnableList[Random.Range(0, _spawnableList.Count)]);

            spawnedObstacle.UpdateBoundaries(_boundaries.position);
            spawnedObstacle.UpdateForward(_boundaries.forward);

            spawnedObstacle.transform.SetParent(this.gameObject.transform);
            spawnedObstacle.gameObject.SetActive(false);
            spawnedObstacle.OnObstacleDisable += ObstacleBoundReached;
            _currentObstacles.Add(spawnedObstacle);
        }

        for (int i = 0; i < ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxActiveAmount; i++) ExecuteObstacle();
    }

    private void ExecuteObstacle()
    {
        if (_activeObstacles.Count >= ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxActiveAmount) return;

        var obstacle = _currentObstacles[Random.Range(0, _currentObstacles.Count)];
        Vector3 randPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
        randPosition += new Vector3(
            Random.Range(-ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMinRandomOffset.x, ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxRandomOffset.x),
            Random.Range(-ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMinRandomOffset.y, ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxRandomOffset.y),
            Random.Range(-ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMinRandomOffset.z, ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxRandomOffset.z));

        obstacle.transform.position = randPosition;

        _currentObstacles.Remove(obstacle);
        _activeObstacles.Add(obstacle);

        obstacle.gameObject.SetActive(true);
        obstacle.UpdateSpeeds(
            Random.Range(ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMinRandomSpeed, ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxRandomSpeed),
            Random.Range(ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMinRandomMaxSpeed, ScenarioPersistentData.SceneData.SceneObstacles.ObstacleMaxRandomMaxSpeed)
            );
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _currentObstacles.Count; i++) 
            _currentObstacles[i].OnObstacleDisable -= ObstacleBoundReached;

        for (int i = 0; i < _activeObstacles.Count; i++)
            _activeObstacles[i].OnObstacleDisable -= ObstacleBoundReached;
    }

    private void ObstacleBoundReached(ObstacleObject obsObject)
    {
        _activeObstacles.Remove(obsObject);
        obsObject.gameObject.SetActive(false);
        _currentObstacles.Add(obsObject);
        ExecuteObstacle();
    }
}
