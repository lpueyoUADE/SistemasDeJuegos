using System.Collections.Generic;
using UnityEngine;

public class PoolGenerics
{
    // Projectile pool
    public class ProjectilePoolEntry<T>
    {
        public WeaponType Type;
        public T Value;
    }

    public class ProjectilePool<T>
    {
        private List<ProjectilePoolEntry<T>> _availables = new List<ProjectilePoolEntry<T>>();
        private List<ProjectilePoolEntry<T>> _inUse = new List<ProjectilePoolEntry<T>>();

        public ProjectilePoolEntry<T> GetOrCreate(WeaponType type)
        {
            // Get if the pools contains any projectile.
            if (_availables.Count > 0)
            {
                for (int i = 0; i < _availables.Count; i++)
                {
                    if (_availables[i].Type == type)
                    {
                        var projectile = _availables[i];
                        _availables.RemoveAt(i);
                        _inUse.Add(projectile);
                        return projectile;
                    }
                }
            }

            // Create if no coincidence was found.
            var newProjectile = new ProjectilePoolEntry<T> { Type = type };
            _inUse.Add(newProjectile);
            return newProjectile;
        }

        public void InUseToAvailable(ProjectilePoolEntry<T> poolEntry)
        {
            _inUse.Remove(poolEntry);
            _availables.Add(poolEntry);
        }
    }

    // Item pool
    public class ItemPoolEntry<T>
    {
        public ConsumableType ItemConsumable;
        public WeaponType ItemWeapon;
        public T Value;
    }

    public class ItemPool<T>
    {
        private List<ItemPoolEntry<T>> _availables = new List<ItemPoolEntry<T>>();
        private List<ItemPoolEntry<T>> _inUse = new List<ItemPoolEntry<T>>();

        public ItemPoolEntry<T> GetOrCreate(ConsumableType consumableType)
        {
            // Get if the pools contains any item.
            if (_availables.Count > 0)
            {
                for (int i = 0; i < _availables.Count; i++)
                {
                    if (_availables[i].ItemConsumable == consumableType)
                    {
                        var consumable = _availables[i];
                        _availables.RemoveAt(i);
                        _inUse.Add(consumable);
                        return consumable;
                    }
                }
            }

            // Create if no coincidence was found.
            var newItemConsumable = new ItemPoolEntry<T> { ItemConsumable = consumableType, ItemWeapon = WeaponType.None };
            _inUse.Add(newItemConsumable);
            return newItemConsumable;
        }

        public ItemPoolEntry<T> GetOrCreate(WeaponType weaponType)
        {
            if (_availables.Count > 0)
            {
                for (int i = 0; i < _availables.Count; i++)
                {
                    if (_availables[i].ItemWeapon == weaponType)
                    {
                        var weapon = _availables[i];
                        _availables.RemoveAt(i);
                        _inUse.Add(weapon);
                        return weapon;
                    }
                }
            }

            // Create if no coincidence was found.
            var newItemWeapon = new ItemPoolEntry<T> { ItemConsumable = ConsumableType.None, ItemWeapon = weaponType };
            _inUse.Add(newItemWeapon);
            return newItemWeapon;
        }

        public void InUseToAvailable(ItemPoolEntry<T> poolEntry)
        {
            _inUse.Remove(poolEntry);
            _availables.Add(poolEntry);
        }
    }
}
