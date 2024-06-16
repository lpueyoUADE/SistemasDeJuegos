using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Scene references
    [SerializeField] private ShipDatabase _playerPrefab;
    [SerializeField] private PlayerCamera _playerCamera;

    // References
    [SerializeField] private List<WeaponDatabase> _weaponsList = new List<WeaponDatabase>();
    [SerializeField] private List<ProjectileBase> _projectileList = new List<ProjectileBase>();
    [SerializeField] private List<EnemyBase> _enemyList = new List<EnemyBase>();
    [SerializeField] private UIManager _uiManager;

    // Static instances
    private static Pool _pool;
    private static FactoryProjectiles _factoryProjectile;
    private static FactoryWeapon _factoryWeapon;
    //private static EnemyFactory _enemyFactory;

    private void Awake()
    {
        SubEvents();

        // Initialize instances
        _factoryProjectile = GetComponent<FactoryProjectiles>();
        _factoryProjectile.UpdateAvailableProjectiles(_projectileList);

        _pool = new Pool(_projectileList, _factoryProjectile);
        _factoryWeapon = new FactoryWeapon(_weaponsList);
        //_enemyFactory = new EnemyFactory(_enemyList);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
        PlayerEvents.OnPlayerSpawn += EventOnPlayerSpawned;
        PlayerEvents.OnPlayerHPUpdate += EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath += EventOnPlayerDeath;
    }

    private void UnsubEvents()
    {
        PlayerEvents.OnPlayerSpawn -= EventOnPlayerSpawned;
        PlayerEvents.OnPlayerHPUpdate -= EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath -= EventOnPlayerDeath;
    }

    private void Start()
    {
        Instantiate(_playerPrefab.Prefab);

        UIEvents.OnAllWeaponsInitialize.Invoke(_weaponsList);        

        _playerCamera.enabled = true;
    }

    private void EventOnPlayerSpawned(PlayerController reference)
    {
        _playerCamera._player = reference;
        UIEvents.OnPlayerSpawn.Invoke();
    }

    private void EventOnPlayerHPUpdate(float currentLife)
    {
        UIEvents.OnPlayerHPUpdate.Invoke(currentLife);
    }

    private void EventOnPlayerDeath()
    {
        UIEvents.OnPlayerDeath.Invoke();
    }
}
