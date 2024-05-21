using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<WeaponDatabase> _weaponsList = new List<WeaponDatabase>();
    [SerializeField] private List<ProjectileBase> _projectileList = new List<ProjectileBase>();
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    // Static instances
    private static GameManager _instance;
    private static Pool _pool;
    private static FactoryProjectiles _factoryProjectile;
    private static FactoryWeapon _factoryWeapon;
    private static EnemyFactory _enemyFactory;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Initialize instances
        _factoryProjectile = GetComponent<FactoryProjectiles>();
        _factoryProjectile.UpdateAvailableProjectiles(_projectileList);

        _pool = new Pool(_projectileList, _factoryProjectile);
        _factoryWeapon = new FactoryWeapon(_weaponsList);
        _enemyFactory = new EnemyFactory(_enemyList);
    }
}
