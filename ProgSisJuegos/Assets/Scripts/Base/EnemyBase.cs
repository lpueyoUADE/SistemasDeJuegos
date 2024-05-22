using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{
    [SerializeField] protected ShipType _type = ShipType.CannonFoder;



    /*
    protected virtual void Update()
    {
        if (_currentTimeToFire < _data.FireRate)
        {
            _currentTimeToFire += Time.deltaTime;
        }
        else if (_currentTimeToFire >= _data.FireRate)
        {
            //Fire();
            Debug.Log("Fire at player");
            _currentTimeToFire = 0;
        }
    }
    */
}