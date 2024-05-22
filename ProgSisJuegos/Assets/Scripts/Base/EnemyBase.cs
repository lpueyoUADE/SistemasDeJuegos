using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ShipBase
{    
   //protected EnemyDatabase _data;
   protected GameObject _player;
   protected float _currentTimeToFire;
   protected EnemyDatabase _data;


   [HideInInspector]public EnemyDatabase Data => _data;


    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _data = _shipData as EnemyDatabase;
    }

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


}
