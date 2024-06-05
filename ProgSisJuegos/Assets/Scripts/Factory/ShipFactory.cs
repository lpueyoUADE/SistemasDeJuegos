using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    private static List<ShipBase> _ships = new List<ShipBase>();

    public ShipFactory(List<ShipBase> shipList)
    {
        _ships = shipList;
    }

    public void UpdateAvailableships(List<ShipBase> ships)
    {
        Debug.Log($"Factory: ships initialized, {ships.Count} items.");
        _ships = ships;
    }

    /*
    public GameObject GenerateEnemy(EnemyType type)
    {
        for (int i = 0; i < _ships.Count; i++)
            if (_ships[i].GetComponent<EnemyBase>().Data.EnemyType == type)
                return Instantiate(_ships[i]);

        Debug.Log($"Factory: No enemy of type {type} was found.");
        return null;
    }
    */

    public GameObject CreateEnemy(ShipType requestedEnemy)
    {
        GameObject result;
        result = EnemyPool.GetEnemy(requestedEnemy);

        if (result == null)
        {
            for (int i = 0; i < _ships.Count; i++)
                if (_ships[i].GetComponent<ShipBase>().ShipData.Type == requestedEnemy)
                    return Instantiate(_ships[i].gameObject);
        }

        return result;
    }

}
