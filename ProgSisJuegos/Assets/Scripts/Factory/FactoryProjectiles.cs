using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryProjectiles : MonoBehaviour
{
    private static List<ProjectileBase> _projectiles = new List<ProjectileBase>();

    public static void UpdateAvailableProjectiles(List<ProjectileBase> projectiles)
    {
        _projectiles.Clear();
        string message = "Factory Projectiles: \n";
        foreach (ProjectileBase projectile in projectiles)
            message += $"{projectile.ProjectileType}, ";

        _projectiles = projectiles;
        Debug.Log($"{message} Initialized {_projectiles.Count} items.");
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
