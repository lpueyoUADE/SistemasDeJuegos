using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<ShipBase> _shipList = new List<ShipBase>();

    // Scene references
    [SerializeField] private ShipDatabase _playerPrefab;
    [SerializeField] private PlayerCamera _playerCamera;

    // References
    [SerializeField] private List<WeaponDatabase> _weaponsList = new List<WeaponDatabase>();
    [SerializeField] private List<ProjectileBase> _extraProjectiles = new List<ProjectileBase>();
    [SerializeField] private List<ShipDatabase> _enemyList = new List<ShipDatabase>();

    // Static instances
    private static GameManager _instance;
    private static Pool _pool;
    private static FactoryProjectiles _factoryProjectile;
    private static FactoryWeapon _factoryWeapon;
    private static ShipFactory _shipFactory;


    // Values
    private float _currentScore = 0;

    private void Awake()
    {
        SubEvents();
        FactoryWeapon.InitializeFactoryWeapons(_weaponsList);
       _shipFactory = new ShipFactory(_shipList);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
        GameManagerEvents.createEnemyDelegate += SpawnEnemy;
        GameManagerEvents.OnEnemyDestroyed += EventOnEnemyDestroyed;
        PlayerEvents.OnPlayerSpawn += EventOnPlayerSpawned;
        PlayerEvents.OnPlayerHPUpdate += EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath += EventOnPlayerDeath;
    }

    private void UnsubEvents()
    {
        GameManagerEvents.OnEnemyDestroyed -= EventOnEnemyDestroyed;
        PlayerEvents.OnPlayerSpawn -= EventOnPlayerSpawned;
        PlayerEvents.OnPlayerHPUpdate -= EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath -= EventOnPlayerDeath;
    }

    private void Start()
    {
        List<ProjectileBase> weaponProjectiles = new List<ProjectileBase>();
        foreach (WeaponDatabase weapon in _weaponsList)
        {
            if (weapon.WeapType == WeaponType.None) continue;
            weaponProjectiles.Add(weapon.WeapProjectilePrefab);
        }

        foreach (ProjectileBase projectile in _extraProjectiles) weaponProjectiles.Add(projectile);

        FactoryProjectiles.UpdateAvailableProjectiles(weaponProjectiles);
        Pool.InitializePool(weaponProjectiles);

        Instantiate(_playerPrefab.Prefab);
        UIEvents.OnAllWeaponsInitialize.Invoke(_weaponsList);
        _playerCamera.enabled = true;
    }

    private void EventOnPlayerSpawned(PlayerController reference)
    {
        _playerCamera._player = reference;
        UIEvents.OnPlayerSpawn.Invoke();
    }

    private void EventOnPlayerHPUpdate(float currentLife, float maxLife)
    {
        UIEvents.OnPlayerHPUpdate.Invoke(currentLife, maxLife);
    }

    private void EventOnPlayerDeath()
    {
        UIEvents.OnPlayerDeath.Invoke();
    }

    private void EventOnEnemyDestroyed(float points)
    {
        _currentScore += points;
        UIEvents.OnScoreUpdate(_currentScore);
    }

    private GameObject SpawnEnemy(ShipDatabase ship)
    {
        return _shipFactory.CreateEnemy(ship.Type);

    }
}
