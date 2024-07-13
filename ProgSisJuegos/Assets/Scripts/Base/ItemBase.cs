using System;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{
    [SerializeField] private ItemDatabase _itemData;
    [SerializeField] private float _rotationSpeed = 2;

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
        UIEvents.OnPlayUISound(_itemData.ItemGrabSound, 1);
        PlayerEvents.OnItemGrab.Invoke(ItemData);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        OnSleep?.Invoke();
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.up * _rotationSpeed);
    }
}