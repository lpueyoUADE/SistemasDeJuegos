using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GeneralSoundsDatabase _soundsToUse;

    // Scene references
    [SerializeField] private ShipDatabase _playerPrefab;
    [SerializeField] private PlayerCamera _playerCamera;

    // References
    [SerializeField] private List<WeaponDatabase> _weaponsList = new List<WeaponDatabase>();
    [SerializeField] private List<ProjectileBase> _extraProjectiles = new List<ProjectileBase>();
    [SerializeField] private List<ItemDatabase> _itemsList = new List<ItemDatabase>();
    [SerializeField] private List<ShipDatabase> _enemyList = new List<ShipDatabase>();

    // Values
    private bool _isGamePaused = false;
    private float _currentScore = 0;

    private void Awake()
    {
        SubEvents();
        FactoryWeapon.InitializeFactoryWeapons(_weaponsList);
        Sounds.UpdateDatabase(_soundsToUse);
    }

    private void OnDestroy()
    {
        UnsubEvents();
    }

    private void SubEvents()
    {
        GameManagerEvents.OnEnemyDestroyed += EventOnEnemyDestroyed;
        GameManagerEvents.CreateEnemy += SpawnShip;
        GameManagerEvents.OnGameResume += PauseUnpaseGame;

        PlayerEvents.OnPlayerSpawn += EventOnPlayerSpawned;
        PlayerEvents.OnWeaponSwap += EventOnPLayerSwapWeapon;
        PlayerEvents.OnPlayerHPUpdate += EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath += EventOnPlayerDeath;

    }

    private void UnsubEvents()
    {
        GameManagerEvents.OnEnemyDestroyed -= EventOnEnemyDestroyed;
        GameManagerEvents.CreateEnemy -= SpawnShip;
        GameManagerEvents.OnGameResume -= PauseUnpaseGame;

        PlayerEvents.OnPlayerSpawn -= EventOnPlayerSpawned;
        PlayerEvents.OnWeaponSwap -= EventOnPLayerSwapWeapon;
        PlayerEvents.OnPlayerHPUpdate -= EventOnPlayerHPUpdate;
        PlayerEvents.OnPlayerDeath -= EventOnPlayerDeath;    
    }

    private void Start()
    {
        InitializeInstances();
        Instantiate(UserSettings.Instance.PlayerShip);

        UIEvents.OnAllWeaponsInitialize.Invoke(_weaponsList);
        _playerCamera.enabled = true;
        gameObject.GetComponent<EnemyManager>().SetCenter(_playerCamera.transform.position);
    }

    private void Update()
    {
        // TODO: add some conditions to pause the game
        if (Input.GetButtonDown("Cancel")) PauseUnpaseGame();
    }

    private void PauseUnpaseGame()
    {
        _isGamePaused = !_isGamePaused;

        UIEvents.OnGamePaused.Invoke(_isGamePaused);
        if (_isGamePaused) Time.timeScale = 0;
        else Time.timeScale = 1;
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

        //BaseEnemies
        List<EnemyBase> enemies = new List<EnemyBase>();
        foreach (ShipDatabase enemy in _enemyList)
        {
            if (enemy.Type == ShipType.None) continue;
            enemies.Add(enemy.Prefab.GetComponent<EnemyBase>());
        }
        ShipFactory.InitializeFactoryShips(_enemyList);

        // Initialize pools
        Pool.InitializePool(weaponProjectiles);
        Pool.InitializePool(items);
        Pool.InitializePool(enemies);
    }

    private void EventOnPlayerSpawned(PlayerController reference)
    {
        _playerCamera._player = reference;
        UIEvents.OnPlayerSpawn.Invoke();
    }

    private void EventOnPLayerSwapWeapon(WeaponType type)
    {
        UIEvents.OnWeaponSwap?.Invoke(type);
    }

    private void EventOnPlayerHPUpdate(float currentLife, float maxLife)
    {
        UIEvents.OnPlayerHPUpdate.Invoke(currentLife, maxLife);
    }

    private void EventOnPlayerDeath()
    {
        UIEvents.OnGameEnded.Invoke(false);
    }

    private void EventOnEnemyDestroyed(float points)
    {
        _currentScore += points;
        UIEvents.OnScoreUpdate(_currentScore);
    }

    private void SpawnShip(ShipDatabase ship, Vector3 spawnPosition)
    {
        //if (shipToSpawn == ShipType.None || locationToSpawn == null) return;
        //Instantiate(ship.Prefab.gameObject, spawnPosition, Quaternion.identity);

        var generatedObject = Pool.CreateShip(ship.Type);
        generatedObject.transform.position = spawnPosition;
        generatedObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

# if UNITY_EDITOR
    public ShipType shipToSpawn = ShipType.None;
    public WeaponType projectileToSpawn = WeaponType.None;
    public ConsumableType consumableItemToSpawn = ConsumableType.None;
    public WeaponType weaponItemToSpawn = WeaponType.None;
    public Transform locationToSpawn;

    [ContextMenu("Spawn Ship")]
    private void SpawnShipDevelop(ShipDatabase ship, Vector3 spawnPosition)
    {
        //if (shipToSpawn == ShipType.None || locationToSpawn == null) return;
        //Instantiate(ship.Prefab.gameObject, spawnPosition, Quaternion.identity);

        var generatedObject = Pool.CreateShip(ship.Type);
        generatedObject.transform.position = spawnPosition;
        generatedObject.transform.rotation = Quaternion.Euler(0,180,0);

        Debug.Log($"Spawing {generatedObject.name} at {locationToSpawn.position}.");

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
