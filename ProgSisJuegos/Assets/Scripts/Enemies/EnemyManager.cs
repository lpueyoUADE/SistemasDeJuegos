using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _index = 0;
    private int _enemiesOnScreen = 0;
    [SerializeField] private List<EnemyWaveDataBase> _enemyWaves;
    private bool startUpdate = false;

    private float totalWaitTime = 60;
    private float currentWaitTime;

    private void Update()
    {
        if (startUpdate)
        {
            if (_enemiesOnScreen == 0)
            {
                LoadNextEnemyWave();
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
    }

    private void LoadNextEnemyWave()
    {
        if (_index < _enemyWaves.Count)
        {
            foreach (EnemyBase enemy in _enemyWaves[_index].Enemies)
            {
                if (enemy != null)
                {
                    GameManager.Instance.SpawnEnemy(enemy.ShipData);
                    _enemiesOnScreen++;
                    //enemy += EnemyDestroyed;
                    enemy.OnDisabled += EnemyDestroyed;
                }
            }
            _index++;
        }
        else if (_index == _enemyWaves.Count)
        {
            //no more enemy waves, level cleared
            print("Level Cleared!");
        }
       
    }

    private void EnableUpdate()
    {
        startUpdate = true;
    }

    private void EnemyDestroyed()
    {
        _enemiesOnScreen--;
    }
}
