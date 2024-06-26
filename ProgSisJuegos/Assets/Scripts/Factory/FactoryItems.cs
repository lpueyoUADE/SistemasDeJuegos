using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryItems : MonoBehaviour
{
    private static List<ItemBase> _items = new List<ItemBase>();

    public static void UpdateAvailableItems(List<ItemBase> items)
    {
        string message = "Factory Items: \n";
        foreach (ItemBase item in items)
            message += $"{item.name}, ";

        _items = items;
        Debug.Log($"{message} Initialized {_items.Count} items.");
    }

    public static ItemBase GenerateItem(ConsumableType type)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemData.ItemType == ItemType.None || _items[i].ItemData.ItemType == ItemType.Weapon) continue;
            if (_items[i].ItemConsumableData?.ItemConsumableType == type)
                return Instantiate(_items[i]);
        }

        Debug.Log($"Factory: No consumable item of type {type} was found.");
        return null;
    }

    public static ItemBase GenerateItem(WeaponType type)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemData.ItemType == ItemType.None || _items[i].ItemData.ItemType == ItemType.Consumable) continue;
            if (_items[i].ItemWeaponData?.ItemWeaponType == type)
                return Instantiate(_items[i]);
        }

        Debug.Log($"Factory: No weapon item of type {type} was found.");
        return null;
    }
}
