using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour, IDamageable, IShip
{
    [SerializeField] protected ShipDatabase _shipData;
    [SerializeField] private Transform _projectileOut;

    private Rigidbody _rBody;
    private List<IWeapon> _weaponList = new List<IWeapon>();
    private IWeapon _currentWeapon;

    protected float _currentLife = 1;
    private bool _isShielded = false;

    public ShipDatabase ShipData => _shipData;
    public List<IWeapon> ShipWeapons => _weaponList;
    public IWeapon ShipCurrentWeapon => _currentWeapon;
    public Transform ShipProyectileOut => _projectileOut;
    public float ShipCurrentLife => _currentLife;
    public bool ShipIsShielded => _isShielded;

    //Events
    public event Action OnDestroyed;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        InitializeWeapons();
    }

    protected virtual void Update()
    {
        float delta = Time.deltaTime;
        Recoil(delta);
    }

    public virtual void InitializeWeapons()
    {
        AddWeapon(ShipData.DefaultWeapon);
        _currentWeapon = _weaponList[0];
    }

    public virtual void Move(Vector3 direction)
    {
        _rBody.AddForce(direction * _shipData.Acceleration, ForceMode.Acceleration);
    }

    public virtual void Move(Vector3 direction, float speed, ForceMode type = ForceMode.Acceleration)
    {
        _rBody.AddForce(direction * speed, type);
    }

    public virtual void Shield(float duration, Color color)
    {
        throw new System.NotImplementedException();
    }

    public virtual void AddWeapon(WeaponType type)
    {
        _weaponList.Add(FactoryWeapon.CreateWeapon(type));
    }

    public virtual void SwapWeapon(bool isNext = true)
    {
        if (ShipWeapons.Count <= 1) return;

        bool swap = false;

        // Check if the next weapon is valid, if not, then set current weapon to the first one.
        if (isNext)
        {
            for (int i = 0; i < ShipWeapons.Count - 1; i++)
            {
                if (ShipCurrentWeapon == ShipWeapons[i])
                    swap = true;

                if (swap && ShipWeapons[i + 1] != null)
                    _currentWeapon = ShipWeapons[i + 1];

                else
                    _currentWeapon = ShipWeapons[0];
            }
        }

        // Check if the previous weapon is valid, if not, then set current weapon to the last one.
        else
        {
            for (int i = ShipWeapons.Count - 1; i > 0; i--)
            {
                if (ShipCurrentWeapon == ShipWeapons[i])
                    swap = true;

                if (swap && ShipWeapons[i - 1] != null)
                    _currentWeapon = ShipWeapons[i - 1];
                else
                    _currentWeapon = ShipWeapons[ShipWeapons.Count - 1];
            }
        }
    }

    public virtual void SwapWeapon(WeaponType type)
    {
        // Directly switch to X weapon
        for (int i = 0; i < ShipWeapons.Count; i++)
        {
            if (ShipWeapons[i].WeaponType == type)
                _currentWeapon = ShipWeapons[i];
        }
    }

    public virtual void Fire()
    {
        _currentWeapon?.Fire(ShipProyectileOut);
    }

    public virtual void Recoil(float delta)
    {
        _currentWeapon?.Recoil(delta);
    }

    public virtual void AnyDamage(float amount)
    {
        _currentLife -= amount;
        if (_currentLife <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        OnDestroyed();
        Destroy(this.gameObject);
    }
}
