using UnityEngine;

[CreateAssetMenu(fileName = "NewItemWeaponData", menuName = "Databases/Items/Weapon")]
public class ItemWeaponDatabase : ItemDatabase
{
    [Header("Weapon settings")]
    [SerializeField] private WeaponType _weaponType = WeaponType.None;

    public WeaponType ItemWeaponType => _weaponType;
}
