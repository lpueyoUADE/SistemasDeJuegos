using UnityEngine;

public enum ItemType
{
    None, Weapon, Consumable
}

public enum ConsumableType
{
    None, Shield, Repair, ExtraLife
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Databases/Items/Generic")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private ItemBase _prefab;

    [Header("Item Settings")]
    [SerializeField] private ItemType _type = ItemType.None;
    [SerializeField] private float _value;
    [SerializeField] private AudioClip _grabSound;

    public ItemBase ItemPrefab => _prefab;
    public ItemType ItemType => _type;
    public float ItemValue => _value;
    public AudioClip ItemGrabSound => _grabSound;
}
