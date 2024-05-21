using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static List<GameObject> _enemies = new List<GameObject>();

    public void UpdateAvailableEnemies(List<GameObject> enemies)
    {
        Debug.Log($"Factory: Projectiles initialized, {enemies.Count} items.");
        _enemies = enemies;
    }

    public GameObject GenerateEnemy(EnemyType type)
    {
        for (int i = 0; i < _enemies.Count; i++)
            if (_enemies[i].GetComponent<EnemyBase>().EnemyType == type)
                return Instantiate(_enemies[i]);

        Debug.Log($"Factory: No enemy of type {type} was found.");
        return null;
    }
}
