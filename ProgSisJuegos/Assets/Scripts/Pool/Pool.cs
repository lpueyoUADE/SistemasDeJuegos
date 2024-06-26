using System.Collections.Generic;
using UnityEngine;
using static PoolGenerics;

public class Pool
{
    private static Dictionary<WeaponType, ProjectileBase> _projectiles = new Dictionary<WeaponType, ProjectileBase>();
    private static Dictionary<ConsumableType, ItemBase> _consumableItems = new Dictionary<ConsumableType, ItemBase>();
    private static Dictionary<WeaponType, ItemBase> _weaponItems = new Dictionary<WeaponType, ItemBase>();
    private static Dictionary<ShipType, EnemyBase> _enemies = new Dictionary<ShipType, EnemyBase>();

    // Pools
    private static ProjectilePool<ProjectileBase> _projectilesPool = new ProjectilePool<ProjectileBase>();
    private static ItemPool<ItemBase> _itemsPool = new ItemPool<ItemBase>();
    private static EnemyPool<EnemyBase> _enemiesPool = new EnemyPool<EnemyBase>();

    public static void InitializePool(List<ProjectileBase> projectiles)
    {
        foreach (ProjectileBase projectile in projectiles)
        {
            if (_projectiles.ContainsKey(projectile.ProjectileType)) continue;
            _projectiles.Add(projectile.ProjectileType, projectile);
        }

        _projectilesPool = new ProjectilePool<ProjectileBase>();
    }

    public static void InitializePool(List<ItemBase> items)
    {
        foreach (ItemBase item in items)
        {
            if (item.ItemData.ItemType == ItemType.None) continue;

            if (item.ItemData.ItemType == ItemType.Consumable)
            {
                if (_consumableItems.ContainsKey(item.ItemConsumableData.ItemConsumableType)) continue;
                _consumableItems.Add(item.ItemConsumableData.ItemConsumableType, item);
            }

            if (item.ItemData.ItemType == ItemType.Weapon)
            {
                if (_weaponItems.ContainsKey(item.ItemWeaponData.ItemWeaponType)) continue;
                _weaponItems.Add(item.ItemWeaponData.ItemWeaponType, item);
            }
        }

        _itemsPool = new ItemPool<ItemBase>();
    }

    //Enemy Pool------------------------------------
    public static void InitializePool(List<EnemyBase> enemies)
    {
        foreach (EnemyBase ship in enemies)
        {
            if (_enemies.ContainsKey(ship.ShipData.Type)) continue;
            _enemies.Add(ship.ShipData.Type, ship);
        }

        _enemiesPool = new EnemyPool<EnemyBase>();
    }

    public static ProjectileBase CreateProjectile(WeaponType type)
    {
        var projectile = _projectilesPool.GetOrCreate(type);

        // Get a new projectile & listen to sleep event
        if (projectile.Value == null) 
        {
            projectile.Value = FactoryProjectiles.GenerateProjectile(type);

            projectile.Value.OnSleep += () =>
            {
                projectile.Value.gameObject.SetActive(false);
                _projectilesPool.InUseToAvailable(projectile);
            };

        }

        projectile.Value.gameObject.SetActive(true);
        return projectile.Value;
    }

    public static ItemBase CreateItem(ConsumableType consumableType)
    {
        var consumable = _itemsPool.GetOrCreate(consumableType);

        // Get a new item consumable & listen to sleep event
        if (consumable.Value == null)
        {
            consumable.Value = FactoryItems.GenerateItem(consumableType);

            consumable.Value.OnSleep += () =>
            {
                consumable.Value.gameObject.SetActive(false);
                _itemsPool.InUseToAvailable(consumable);
            };
        }

        consumable.Value.gameObject.SetActive(true);
        return consumable.Value;
    }

    public static ItemBase CreateItem(WeaponType weaponType)
    {
        var weapon = _itemsPool.GetOrCreate(weaponType);

        // Get a new item weapon & listen to sleep event
        {
            if (weapon.Value == null)
            {
                weapon.Value = FactoryItems.GenerateItem(weaponType);

                weapon.Value.OnSleep += () =>
                {
                    weapon.Value.gameObject.SetActive(false);
                    _itemsPool.InUseToAvailable(weapon);
                };
            }
        }

        weapon.Value.gameObject.SetActive(true);
        return weapon.Value;
    }

    //Create Enemy
    public static EnemyBase CreateShip(ShipType type)
    {
        var ship = _enemiesPool.GetOrCreate(type);

        // Get a new projectile & listen to sleep event
        if (ship.Value == null)
        {
            ship.Value = ShipFactory.CreateEnemy(type);

            ship.Value.OnDisabled += () =>
            {
                ship.Value.gameObject.SetActive(false);
                _enemiesPool.InUseToAvailable(ship);
            };

        }

        ship.Value.gameObject.SetActive(true);
        return ship.Value;
    }
}
