using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    public GameObject SpawnEnemy(int requestedEnemy, Vector3 position)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<IEnemy>().ID == requestedEnemy)
            {
                return enemy;
            }
        }

        return default; //Call Factory
    }
}
