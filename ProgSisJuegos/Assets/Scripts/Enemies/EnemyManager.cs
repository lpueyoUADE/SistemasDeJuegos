using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _index = 0;
    private int _enemiesOnScreen = 0;
    [SerializeField] private List<EnemyWaveDataBase> _enemyWaves;
    private bool startUpdate = false;
    private Vector3 _horizontalCenter;

    public Action noEnemiesOnScreen;
    public Action enemiesSpawned;

    private float totalWaitTime = 2;
    private float currentWaitTime;

    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    public float timeToNextWave = 0;
    private float currentTimeToNextWave = 0;

    private void Update()
    {
        if (startUpdate)
        {
            if (_enemiesOnScreen == 0)
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

                    /*GameObject temp = */GameManagerEvents.CreateEnemy.Invoke(enemy.ShipData, (_horizontalCenter + new Vector3(_horizontalOffset,30,50)));
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
}
