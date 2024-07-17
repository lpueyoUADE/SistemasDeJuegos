using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _index = 0;
    private int _enemiesOnScreen = 0;
    [SerializeField] private List<EnemyWaveDataBase> _enemyWaves;
    [SerializeField] private GameObject _levelBoss;
    private bool bossSpawned = false;
    private bool startUpdate = false;
    private Vector3 _horizontalCenter;
    [SerializeField] float spawnDistanceFromPlayer;

    public Action noEnemiesOnScreen;
    public Action enemiesSpawned;

    private float totalWaitTime = 2;
    private float currentWaitTime;

    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    private float timeToNextWave = 0;
    private float currentTimeToNextWave = 0;

    private void Update()
    {
        if (startUpdate)
        {
            if (_enemiesOnScreen == 0 && bossSpawned == false)
            {
                if (currentTimeToNextWave >= timeToNextWave)
                {
                    LoadNextEnemyWave();
                    timeToNextWave = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
                    currentTimeToNextWave = 0;
                }
                else
                {
                    currentTimeToNextWave += Time.deltaTime;
                }
            }
            

        }
        else
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= totalWaitTime)
            {
                Initialize();
                print("EnemyManager Initialized");
            }
        }

    }

    private void Initialize()
    {
        LoadNextEnemyWave();
        EnableUpdate();
        GameManagerEvents.OnEnemyDestroyed += EnemyDestroyed;
        print("EnemyManager Initialized");
    }
    
    public void SetCenter(Vector3 pos)
    {
        _horizontalCenter = pos;
    }

    private void LoadNextEnemyWave()
    {
        enemiesSpawned();
        if (_index < _enemyWaves.Count)
        {
            //int enemiesInWave = _enemyWaves[_index].Enemies.Length;
            float _horizontalOffset = -10f;
            foreach (EnemyBase enemy in _enemyWaves[_index].Enemies)
            {
                if (enemy != null)
                {

                    /*GameObject temp = */GameManagerEvents.CreateEnemy.Invoke(enemy.ShipData, (_horizontalCenter + new Vector3(_horizontalOffset,30,spawnDistanceFromPlayer)));
                    //var spawnedEnemy = temp.GetComponent<EnemyBase>();
                    _enemiesOnScreen++;
                    //enemy += EnemyDestroyed;
                    //spawnedEnemy.OnDisabled += EnemyDestroyed;
                    _horizontalOffset += 10f;
                }
            }
            _index++;
        }
        else if (_index == _enemyWaves.Count)
        {
            //no more enemy waves, level cleared
            print("Level Cleared!");
            //UIEvents.OnPlayerWin();
            SpawnBoss();            

        }

        print("Wave: " + (_index - 1));
       
    }

    private void EnableUpdate()
    {
        startUpdate = true;
    }

    private void EnemyDestroyed(float none)
    {
        _enemiesOnScreen--;
        if (_enemiesOnScreen == 0)
        {
            noEnemiesOnScreen();
        }
    }

    private void SpawnBoss()
    {
        Instantiate(_levelBoss, _horizontalCenter + new Vector3(0, 30, 50), Quaternion.identity);
        bossSpawned = true;
    }
}
