using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryProjectiles : MonoBehaviour
{
    private static List<ProjectileBase> _projectiles = new List<ProjectileBase>();

    public void UpdateAvailableProjectiles(List<ProjectileBase> projectiles)
    {
        Debug.Log($"Factory: Projectiles initialized, {projectiles.Count} items.");
        _projectiles = projectiles;
    }

    public ProjectileBase GenerateProjectile(WeaponType type)
    {
        for (int i = 0; i < _projectiles.Count; i++)
            if (_projectiles[i].ProjectileType == type)
                return Instantiate(_projectiles[i]);

        Debug.Log($"Factory: No projectile of type {type} was found.");
        return null;
    }
}
