using System;
using UnityEngine;
using System.Collections.Generic;

public class EnemyCannonAimSimple : MonoBehaviour
{
    public float range;
    public float angle;
    public float rotatingspeed = 4;
    public GameObject rotatingCannon;
    public Transform projectileOut;
    public Vector3 target;
    public bool exec = false;

    public IWeapon currentWeapon;
    public WeaponType weapon = WeaponType.EnemyBlueRail;

    private List<IWeapon> _weaponList = new List<IWeapon>();


    Vector3 Origin => transform.position;
    Vector3 Forward => transform.forward;

    private void Start()
    {
        InitializeWeapons();
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        if (exec)
        {
            currentWeapon?.Fire(projectileOut);
            Vector3 finalDir = new Vector3(-target.x, -target.y, -target.z);
            rotatingCannon.transform.rotation = Quaternion.SlerpUnclamped(rotatingCannon.transform.rotation, Quaternion.LookRotation(finalDir), delta * rotatingspeed);
            exec = false;
        }

        currentWeapon?.Recoil(delta);
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 dirToTarget = other.transform.position - Origin;
        float angleToTarget = Vector3.Angle(Forward, dirToTarget);
        if (angleToTarget <= angle / 2)
        {
            target = other.transform.position - rotatingCannon.transform.position;
            exec = true;
        }

        if (currentWeapon == null)
        {
            currentWeapon = FactoryWeapon.CreateWeapon(weapon);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Origin, range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Origin, Quaternion.Euler(0, angle / 2, 0) * Forward * range);
        Gizmos.DrawRay(Origin, Quaternion.Euler(0, -(angle / 2), 0) * Forward * range);
    }

    public virtual void InitializeWeapons()
    {
        AddWeapon(weapon);
        currentWeapon = _weaponList[0];
    }

    public virtual void AddWeapon(WeaponType type, int ammo = 0)
    {
        foreach (IWeapon weapon in _weaponList)
        {
            if (weapon.WeaponType != type) continue;
            Debug.Log($"Adding {ammo} of ammo to {type}.");
            weapon.UpdateAmmo(ammo);
            return;
        }

        IWeapon newWeap = FactoryWeapon.CreateWeapon(type);
        newWeap.InitializeWeapon(FactoryWeapon.GetWeaponData(type));
        _weaponList.Add(newWeap);
    }
}
