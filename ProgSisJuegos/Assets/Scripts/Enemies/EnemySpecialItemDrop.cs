using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialItemDrop : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemToSpawn;
    private ItemConsumableDatabase _consumableToSpawn => (ItemConsumableDatabase)_itemToSpawn;
    private ItemWeaponDatabase _weaponToSpawn => (ItemWeaponDatabase)_itemToSpawn;

    private void Awake()
    {
        TryGetComponent(out EnemyBase enemyScript);
        enemyScript.OnDisabled += OnEnemyDestroyed;
    }

    private void OnDestroy()
    {
        TryGetComponent(out EnemyBase enemyScript);
        if (enemyScript != null) enemyScript.OnDisabled -= OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed()
    {
        if (_itemToSpawn == null || _itemToSpawn.ItemType == ItemType.None) return;

        if (_itemToSpawn.ItemType == ItemType.Consumable)
        {
            var item = Pool.CreateItem(_consumableToSpawn.ItemConsumableType);
            item.transform.position = this.transform.position;
            return;
        }

        if (_itemToSpawn.ItemType == ItemType.Weapon)
        {
            var item = Pool.CreateItem(_weaponToSpawn.ItemWeaponType);
            item.transform.position = this.transform.position;
            return;
        }
    }
}
