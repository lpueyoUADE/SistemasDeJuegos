using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _index = 0;
    private int _enemiesOnScreen = 0;
    [SerializeField] private List<EnemyWaveDataBase> _enemyWaves;
    private bool startUpdate = false;

    private float totalWaitTime = 2;
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

                    GameObject temp = GameManagerEvents.createEnemyDelegate.Invoke(enemy.ShipData);
                    var spawnedEnemy = temp.GetComponent<EnemyBase>();
                    _enemiesOnScreen++;
                    //enemy += EnemyDestroyed;
                    spawnedEnemy.OnDisabled += EnemyDestroyed;
                }
            }
            _index++;
        }
        else if (_index == _enemyWaves.Count)
        {
            //no more enemy waves, level cleared
            print("Level Cleared!");
        }

        print("Wave: " + (_index - 1));
       
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
