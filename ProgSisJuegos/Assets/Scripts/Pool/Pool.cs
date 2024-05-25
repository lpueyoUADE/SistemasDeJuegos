using System.Collections.Generic;
using UnityEngine;
using static PoolGenerics;

public class Pool
{
    // References
    private static Dictionary<WeaponType, ProjectileBase> _projectiles = new Dictionary<WeaponType, ProjectileBase>();
    private static FactoryProjectiles _factory;

    // Pools
    private static ProjectilePool<ProjectileBase> _projectilesPool = new ProjectilePool<ProjectileBase>();

    public Pool(List<ProjectileBase> projectiles, FactoryProjectiles factory)
    {
        foreach (ProjectileBase projectile in projectiles)
            _projectiles.Add(projectile.ProjectileType, projectile);

        _factory = factory;
    }
    
    public static ProjectileBase CreateProjectile(WeaponType type)
    {
        var projectile = _projectilesPool.GetOrCreate(type);

        // Get a new projectile & listen to sleep event
        if (projectile.Value == null) 
        {
            projectile.Value = _factory.GenerateProjectile(type);

            projectile.Value.OnSleep += () =>
            {
                projectile.Value.gameObject.SetActive(false);
                _projectilesPool.InUseToAvailable(projectile);
            };
        }

        projectile.Value.gameObject.SetActive(true);
        return projectile.Value;
    }
}
