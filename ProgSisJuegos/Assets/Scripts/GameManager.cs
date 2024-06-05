using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Scene references
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private PlayerCamera _playerCamera;

    // References
    [SerializeField] private List<WeaponDatabase> _weaponsList = new List<WeaponDatabase>();
    [SerializeField] private List<ProjectileBase> _projectileList = new List<ProjectileBase>();
    [SerializeField] private List<ShipBase> _shipList = new List<ShipBase>();
    [SerializeField] private UIManager _uiManager;

    private PlayerController _currentPlayer;

    // Static instances
    private static GameManager _instance;    
    private static Pool _pool;
    private static FactoryProjectiles _factoryProjectile;
    private static FactoryWeapon _factoryWeapon;
    private static ShipFactory _shipFactory;

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
        //_enemyFactory = new EnemyFactory(_enemyList);
       _shipFactory = new ShipFactory(_shipList);
    }

    private void Start()
    {        

        _currentPlayer = Instantiate(_playerPrefab);
        _playerCamera.enabled = true;
        _playerCamera._player = _currentPlayer;

        UIEvents.OnAllWeaponsInitialize.Invoke(_weaponsList);
    }

    public void SpawnEnemy(ShipDatabase ship)
    {
        _shipFactory.CreateEnemy(ship.Type);
    }
}
