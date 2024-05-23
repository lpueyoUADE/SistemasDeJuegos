using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyPool
{
    private static List<GameObject> _enemies = new List<GameObject>();
    
    //private EnemyFactory _factory;


    /*
    public GameObject SpawnEnemy(EnemyType requestedEnemy, Vector3 position)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyBase>().Data.EnemyType == requestedEnemy)
            {
                return enemy;
            }
        }

        return _factory.GenerateEnemy(requestedEnemy);
    }
    */

    public static GameObject GetEnemy(ShipType requestedEnemy)
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].GetComponent<ShipBase>().ShipData.Type == requestedEnemy)
                {
                    var enemy = _enemies[i];
                    _enemies.RemoveAt(i);                    
                    return enemy;
                }
            }
        }

        return null;
    }

    public static void AddEnemy(GameObject enemyToAdd)
    {
        _enemies.Add(enemyToAdd);
    }
}
