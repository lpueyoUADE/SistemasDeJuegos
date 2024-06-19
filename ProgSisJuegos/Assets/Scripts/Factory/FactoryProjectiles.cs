using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryProjectiles : MonoBehaviour
{
    private static List<ProjectileBase> _projectiles = new List<ProjectileBase>();

    public static void UpdateAvailableProjectiles(List<ProjectileBase> projectiles)
    {
        string message = "Factory Projectiles: \n";
        foreach (ProjectileBase projectile in projectiles)
            message += $"{projectile.ProjectileType}, ";

        Debug.Log($"{message} Initialized {projectiles.Count} items.");
        _projectiles = projectiles;
    }

    public static ProjectileBase GenerateProjectile(WeaponType type)
    {
        for (int i = 0; i < _projectiles.Count; i++)
            if (_projectiles[i].ProjectileType == type)
                return Instantiate(_projectiles[i]);

        Debug.Log($"Factory: No projectile of type {type} was found.");
        return null;
    }
}
