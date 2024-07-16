using System;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{
    [SerializeField] private ItemDatabase _itemData;
    [SerializeField] private float _rotationSpeed = 2;
    [SerializeField] private float _movementSpeed = 2;
    private Rigidbody _rBody;
    private Vector3 _forward;

    public ItemDatabase ItemData => _itemData;
    public ItemConsumableDatabase ItemConsumableData => (ItemConsumableDatabase)_itemData;
    public ItemWeaponDatabase ItemWeaponData => (ItemWeaponDatabase)_itemData;

    public Action OnSleep;

    private void Awake()
    {
        _forward = -transform.forward;
        _rBody = GetComponent<Rigidbody>();
    }

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

        if (_rBody!= null)
            _rBody.AddForce(_forward * _movementSpeed, ForceMode.Acceleration);
    }
}