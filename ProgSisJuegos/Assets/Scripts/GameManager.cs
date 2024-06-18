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
    [SerializeField] private List<ProjectileBase> _extraProjectiles = new List<ProjectileBase>();
    [SerializeField] private List<EnemyBase> _enemyList = new List<EnemyBase>();

    // Values
    private float _currentScore = 0;

    private void Awake()
    {
        SubEvents();
        FactoryWeapon.InitializeFactoryWeapons(_weaponsList);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
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

    private void EventOnPlayerHPUpdate(float currentLife)
    {
        UIEvents.OnPlayerHPUpdate.Invoke(currentLife);
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
}
