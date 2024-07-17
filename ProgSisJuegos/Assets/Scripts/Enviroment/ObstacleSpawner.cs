using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObstaclePrefabList;
    [SerializeField] private int randomYAmplitude;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;
    [SerializeField] private float _spawnIntervalModifier;
    [SerializeField] private float noEnemiesModifier;
    [SerializeField] EnemyManager enemyManager;
    private bool modifierOn = false;

    [SerializeField] AsteroidPool _pool;
    private float _currentTime = 0;
    private float _chosenSpawnTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager.noEnemiesOnScreen += ApplyModifier;
        enemyManager.enemiesSpawned += RemoveModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (_chosenSpawnTime == 0)
        {
            _chosenSpawnTime = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }

        if(_currentTime < _chosenSpawnTime && modifierOn)
        {
            _currentTime += Time.deltaTime + noEnemiesModifier;
        }
        else if (_currentTime < _chosenSpawnTime)
        {
            _currentTime += Time.deltaTime;
        }
        else if (_currentTime >= _chosenSpawnTime)
        {

            SpawnAsteroid();


            _chosenSpawnTime = 0;
            _currentTime = 0;
        }
    }

    private void SpawnAsteroid()
    {
        if (_pool.IsPoolEmpty() == false)
        {
            GameObject asteroid = _pool.GetFromPool();
            asteroid.transform.position = this.gameObject.transform.position;
            asteroid.SetActive(true);
        }
        else
        {
            int index = Random.Range(0, ObstaclePrefabList.Count);
            GameObject newObstacle = ObstaclePrefabList[index];

            Vector3 randomY = new (0, Random.Range(-randomYAmplitude, randomYAmplitude), 0);

            GameObject asteroid = Instantiate(newObstacle, transform.position + randomY, Quaternion.identity);
            asteroid.GetComponent<ObstacleScript>().AssignPool(_pool);
        }       
        
    }

    private void ApplyModifier()
    {
        if (modifierOn == false)
        {
            _minSpawnInterval += _spawnIntervalModifier;
            _maxSpawnInterval += _spawnIntervalModifier;
        }
        modifierOn = true;        
    }

    private void RemoveModifier()
    {
        if (modifierOn)
        {
            _minSpawnInterval -= _spawnIntervalModifier;
            _maxSpawnInterval -= _spawnIntervalModifier;
        }
        modifierOn = false;        
    }
    
}
