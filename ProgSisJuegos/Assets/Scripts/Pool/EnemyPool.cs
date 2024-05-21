using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private EnemyFactory _factory;
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
}
