using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    private static Dictionary<ShipType, ShipDatabase> _shipsDict = new Dictionary<ShipType, ShipDatabase>();

    public static void InitializeFactoryShips(List<ShipDatabase> ships)
    {
        string message = "Factory ships: \n";
        foreach (ShipDatabase ship in ships)
        {
            _shipsDict.Add(ship.Type, ship);
            message += $"{ship.Type}, ";
        }

        Debug.Log($"{message} Initialized {_shipsDict.Count} items.");
    }
   
    public static EnemyBase CreateEnemy(ShipType requestedEnemy)
    { 
        _shipsDict.TryGetValue(requestedEnemy, out ShipDatabase data);
                
        GameObject temp =Instantiate(data.Prefab.gameObject);

        return temp.GetComponent<EnemyBase>();
        
    }
 





    /*
    public static Iship Createship(ShipType type)
    {
        _shipsDict.TryGetValue(type, out ShipDatabase data);

        //Debug.Log($"Factory (ships): Trying to create {type} - value {data}.");

        switch (type)
        {
            // Player type
            case ShipType.ElCapitan: return new shipBlueRail(data);
            case ShipType.Sonic: return new shipRedDiamond(data);
            case ShipType.SkullFlower: return new shipGreenCrast(data);

            // Enemy type
            case ShipType.Mosquitoe: return new shipEnemyBlueRail(data);
            case ShipType.Slider: return new shipEnemyBlueRail(data);
            case ShipType.Tremor: return new shipEnemyBlueRail(data);
            case ShipType.CannonFoder: return new shipEnemyBlueRail(data);

            // Not found
            default: Debug.Log($"Factory (ships): ship of type {type} not found."); return new shipNone();
        }

        /*  None,
    ElCapitan,
    Sonic,
    SkullFlower,
    Mosquitoe,
    Slider,
    Tremor,
    CannonFoder,     */
    }



