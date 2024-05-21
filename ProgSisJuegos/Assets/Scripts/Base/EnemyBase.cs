using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
   private EnemyDatabase _data;
    private GameObject _player;

   public EnemyDatabase Data => _data;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


}
