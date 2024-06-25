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
    [SerializeField] private List<ItemDatabase> _itemsList = new List<ItemDatabase>();
    [SerializeField] private List<ShipDatabase> _enemyList = new List<ShipDatabase>();

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
        InitializeInstances();
        Instantiate(_playerPrefab.Prefab);

        UIEvents.OnAllWeaponsInitialize.Invoke(_weaponsList);
        _playerCamera.enabled = true;
    }

    private void InitializeInstances()
    {
        // Initialize Factories

        // Base projectiles
        List<ProjectileBase> weaponProjectiles = new List<ProjectileBase>();
        foreach (WeaponDatabase weapon in _weaponsList)
        {
            if (weapon.WeapType == WeaponType.None) continue;
            weaponProjectiles.Add(weapon.WeapProjectilePrefab);
        }

        // Extra projectiles
        foreach (ProjectileBase projectile in _extraProjectiles) weaponProjectiles.Add(projectile);
        FactoryProjectiles.UpdateAvailableProjectiles(weaponProjectiles);

        // Base items
        List<ItemBase> items = new List<ItemBase>();
        foreach (ItemDatabase item in _itemsList)
        {
            if (item.ItemType == ItemType.None) continue;
            items.Add(item.ItemPrefab);
        }
        FactoryItems.UpdateAvailableItems(items);

        // Initialize pools
        Pool.InitializePool(weaponProjectiles);
        Pool.InitializePool(items);
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

# if UNITY_EDITOR
    public ShipType shipToSpawn = ShipType.None;
    public WeaponType projectileToSpawn = WeaponType.None;
    public ConsumableType consumableItemToSpawn = ConsumableType.None;
    public WeaponType weaponItemToSpawn = WeaponType.None;
    public Transform locationToSpawn;

    [ContextMenu("Spawn Ship")]
    void SpawnShip()
    {
        if (shipToSpawn == ShipType.None || locationToSpawn == null) return;

        Debug.LogWarning($"Not implemented.");
    }

    [ContextMenu("Spawn Projectile")]
    void SpawnProjectile()
    {
        if (projectileToSpawn == WeaponType.None || locationToSpawn == null) return;

        var generatedObject = Pool.CreateProjectile(projectileToSpawn);
        generatedObject.transform.position = locationToSpawn.position;
        generatedObject.transform.rotation = locationToSpawn.rotation;

        Debug.Log($"Spawing {generatedObject.name} at {locationToSpawn.position}.");
    }

    [ContextMenu("Spawn Consumable Item")]
    void SpawnItemConsumable()
    {
        if (consumableItemToSpawn == ConsumableType.None || locationToSpawn == null) return;

        var generatedObject = Pool.CreateItem(consumableItemToSpawn);
        generatedObject.transform.position = locationToSpawn.position;
        generatedObject.transform.rotation = locationToSpawn.rotation;

        Debug.Log($"Spawing {generatedObject.name} at {locationToSpawn.position}.");
    }

    [ContextMenu("Spawn Weapon Item")]
    void SpawnItemWeapon()
    {
        if (weaponItemToSpawn == WeaponType.None || locationToSpawn == null) return;

        var generatedObject = Pool.CreateItem(weaponItemToSpawn);
        generatedObject.transform.position = locationToSpawn.position;
        generatedObject.transform.rotation = locationToSpawn.rotation;

        Debug.Log($"Spawing {generatedObject.name} at {locationToSpawn.position}.");
    }
# endif
}
