using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _index = 0;
    private int _enemiesOnScreen = 0;
    [SerializeField] private List<EnemyWave> _enemyWaves;

    private void Update()
    {
        if(_enemiesOnScreen == 0)
        {
            LoadNextEnemyWave();
        }
    }

    private void LoadNextEnemyWave()
    {
        foreach (ShipBase enemy in _enemyWaves[_index].Enemies)
        {
            if (enemy != null)
            {
               GameManager.Instance.SpawnEnemy(enemy.ShipData);
                _enemiesOnScreen++;
                enemy.OnDestroy += EnemyDestroyed;
            }
        }
        _index++;
    }

    private void EnemyDestroyed()
    {
        _enemiesOnScreen--;
    }


}
