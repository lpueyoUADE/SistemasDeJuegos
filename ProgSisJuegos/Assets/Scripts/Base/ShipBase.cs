using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour, IDamageable, IShip
{
    [Header("Data")]
    [SerializeField] protected ShipDatabase _shipData;

    [Header("References")]
    [SerializeField] protected ShieldBase _shieldObject; 
    [SerializeField] private Transform _projectileOut;

    // References
    private AudioSource _audioSource;
    private Rigidbody _rBody;
    protected ShieldBase _shieldScript;

    // Values
    private List<IWeapon> _weaponList = new List<IWeapon>();
    private IWeapon _currentWeapon;
    private int _weaponIndex;
    protected float _currentLife = 1;
    protected float _shieldTimeLeft = 0;

    // Public values
    public ShipDatabase ShipData => _shipData;
    public List<IWeapon> ShipWeapons => _weaponList;
    public IWeapon ShipCurrentWeapon => _currentWeapon;
    public Transform ShipProyectileOut => _projectileOut;
    public int ShipCurrentWeaponIndex => _weaponIndex;
    public float ShipCurrentLife => _currentLife;
    public float ShipShieldDuration => _shipData.ShieldDuration;
    public Color ShipShieldColor => _shipData.ShieldColor;
    public bool ShipIsShielded => _shieldTimeLeft > 0;

    public AudioSource Audio => _audioSource;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
        _currentLife = _shipData.Life;
        _audioSource = GetComponent<AudioSource>();

        if (_shieldObject != null)
        {
            _shieldObject.TryGetComponent(out ShieldBase shieldScript);
            if (shieldScript != null) _shieldScript = shieldScript;
        }
    }

    protected virtual void OnEnable()
    {
        _currentLife = _shipData.Life;
        _currentWeapon?.Reset();
    }

    protected virtual void OnDisable()
    {
        _shieldObject.gameObject.SetActive(false);
        _currentLife = _shipData.Life;
        _shieldTimeLeft = 0;
    }

    protected virtual void Start()
    {
        _shieldObject.gameObject.SetActive(false);
        InitializeWeapons();
    }

    protected virtual void Update()
    {
        float delta = Time.deltaTime;
        UpdateShield(delta);
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

    public virtual void UpdateShield(float delta)
    {
        if (ShipIsShielded)
        {
            _shieldTimeLeft -= delta;
            if (_shieldScript != null) _shieldScript.UpdateShieldTime(_shieldTimeLeft);
        }

        if (!ShipIsShielded && _shieldObject.gameObject.activeSelf) _shieldObject.gameObject.SetActive(false);
    }

    public virtual void Shield(float duration, Color color)
    {
        _shieldTimeLeft = duration;
        _shieldObject.gameObject.SetActive(true);

        if (_shieldScript != null)
        {
            float integrity = (_currentLife / _shipData.Life) * 1;
            Color result = Color.green;

            if (integrity <= 0.25) result = _shipData.ShieldLowIntegrity;
            if (integrity > 0.25) result = _shipData.ShieldMidIntegrity;
            if (integrity > 0.75) result = _shipData.ShieldHighIntegrity;

            _shieldScript.UpdateShieldColorIntegrity(result);
            _shieldScript.UpdateShieldStats(ShipData.ShieldColor, ShipData.ShieldColorSpeed, duration);
        }
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

    public virtual void RemoveWeapon(IWeapon type)
    {
        _currentWeapon?.Swapped();
        _weaponIndex = 0;
        _currentWeapon = _weaponList[_weaponIndex];
        _weaponList.Remove(type);    
    }

    public virtual void SwapWeapon(bool isNext = true)
    {
        if (ShipWeapons.Count <= 1) return;

        if (isNext)
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
        if (ShipIsShielded)
        {
            _shieldTimeLeft -= _shieldTimeLeft * 0.1f;
            if (_audioSource.time <= 0.25f) _audioSource?.PlayOneShot(Sounds.SoundsDatabase.ProjectileHittingShield, 0.45f);

            _currentLife -= amount / ShipData.DamageResistance;
            Debug.Log($"CL {_currentLife} / INCOMING {amount} - REDUCED: {amount / ShipData.DamageResistance}");
        }
        else _currentLife -= amount;

        if (_currentLife <= 0)
        {
            OnDeath();
            return;
        }

        if (!ShipIsShielded) Shield(ShipShieldDuration, ShipShieldColor);
        if (this.gameObject.activeSelf) _audioSource?.PlayOneShot(Sounds.SoundsDatabase.ProjectileHittingShip);
    }

    public virtual void ShipRepair(float amount)
    {
        if ((_currentLife + amount) < ShipData.Life) _currentLife += amount;
        else _currentLife = ShipData.Life;
    }

    public virtual void OnDeath()
    {
        UniversalPooleableObject explosionSound = Pool.CreateUniversalObject(UniversalPoolObjectType.Audio);
        explosionSound.transform.position = transform.position;
        explosionSound.UpdateAudioAndPlay(Sounds.SoundsDatabase.ShipExplosions);

        print($"{this.name} is dead.");
        this.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    [ContextMenu("Damage by 1")]
    void DamageShip()
    {
        AnyDamage(1);
    }

    [ContextMenu("Destroy ship")]
    void DestroyShip()
    {
        AnyDamage(99999);
    }
#endif
}
