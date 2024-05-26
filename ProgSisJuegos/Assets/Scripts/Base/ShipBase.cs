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

    private int _currentLife = 1;
    private bool _isShielded = false;

    public ShipDatabase ShipData => _shipData;
    public List<IWeapon> ShipWeapons => _weaponList;
    public IWeapon ShipCurrentWeapon => _currentWeapon;
    public int ShipCurrentLife => _currentLife;
    public bool ShipIsShielded => _isShielded;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        AddWeapon(ShipData.DefaultWeapon);
        _currentWeapon = _weaponList[0];
    }

    protected virtual void Update()
    {
        float delta = Time.deltaTime;
        if (_currentWeapon != null) _currentWeapon.Recoil(delta);
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

    public virtual void Fire()
    {
        _currentWeapon?.Fire(_projectileOut.position);
    }

    public virtual void AnyDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }

}
