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
    private int _weaponIndex;

    protected float _currentLife = 1;
    private bool _isShielded = false;

    public ShipDatabase ShipData => _shipData;
    public List<IWeapon> ShipWeapons => _weaponList;
    public IWeapon ShipCurrentWeapon => _currentWeapon;
    public Transform ShipProyectileOut => _projectileOut;
    public int ShipCurrentWeaponIndex => _weaponIndex;
    public float ShipCurrentLife => _currentLife;
    public bool ShipIsShielded => _isShielded;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
        _currentLife = _shipData.Life;
    }

    private void OnEnable()
    {
        _currentLife = _shipData.Life;
        _currentWeapon?.Reset();
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
        if (_rBody != null)
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

        if (!isNext)
        {
            if (_weaponIndex < _weaponList.Count - 1) _weaponIndex++;
            else _weaponIndex = 0;
        }

        else
        {
            if (_weaponIndex <= 0) _weaponIndex = _weaponList.Count - 1;
            else _weaponIndex--;
        }

        _currentWeapon?.Swapped();
        _currentWeapon = _weaponList[_weaponIndex];
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

    public virtual void StopFire()
    {
        _currentWeapon?.StopFire();
    }

    public virtual void Recoil(float delta)
    {
        _currentWeapon?.Recoil(delta);
    }

    public virtual void AnyDamage(float amount)
    {
        _currentLife -= amount;

        if (_currentLife <= 0) OnDeath();
    }

    public virtual void Heal(float amount)
    {
        if ((_currentLife + amount) < ShipData.Life)
        {
            _currentLife += amount;
        }
        else
        {
            _currentLife = ShipData.Life;
        }
        
    }

    public virtual void OnDeath()
    {
        print($"{this.name} is dead.");
        this.gameObject.SetActive(false);
    }
}
