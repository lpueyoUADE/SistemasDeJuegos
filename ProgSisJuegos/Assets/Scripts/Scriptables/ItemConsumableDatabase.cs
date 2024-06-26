using UnityEngine;

[CreateAssetMenu(fileName = "NewItemConsumableData", menuName = "Databases/Items/Consumable")]
public class ItemConsumableDatabase : ItemDatabase
{
    [Header("Consumable Settings")]
    [SerializeField] private ConsumableType _consumableType = ConsumableType.None;

    public ConsumableType ItemConsumableType => _consumableType;
}
