using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;
    [SerializeField] private float noEnemiesModifier;


    [SerializeField] AsteroidPool _pool;
    private float _currentTime = 0;
    private float _chosenSpawnTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_chosenSpawnTime == 0)
        {
            _chosenSpawnTime = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
        if(_currentTime < _chosenSpawnTime)
        {
            _currentTime += Time.deltaTime + noEnemiesModifier;
        }
        else
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
            GameObject asteroid = Instantiate(ObstaclePrefab, transform.position, Quaternion.identity);
            asteroid.GetComponent<ObstacleScript>().AssignPool(_pool);
        }       
        
    }
    
}
