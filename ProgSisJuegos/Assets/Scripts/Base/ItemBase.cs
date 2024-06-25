using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{
    [SerializeField] private ItemDatabase _itemData;
    
    public ItemDatabase ItemData => _itemData;
    public ItemConsumableDatabase ItemConsumableData => (ItemConsumableDatabase)_itemData;
    public ItemWeaponDatabase ItemWeaponData => (ItemWeaponDatabase)_itemData;

    public Action OnSleep;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ItemGrab();
    }

    public virtual void ItemGrab()
    {
        PlayerEvents.OnItemGrab.Invoke(ItemData);
        OnSleep.Invoke();
        //this.gameObject.SetActive(false);
    }
}
