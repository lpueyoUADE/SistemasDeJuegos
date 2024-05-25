using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PoolGenerics
{
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
}
