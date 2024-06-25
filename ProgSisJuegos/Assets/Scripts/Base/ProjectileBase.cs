using System;
using UnityEngine;

public class ProjectileBase : MonoBehaviour, IProjectile
{
    [SerializeField] private string _targetTag = "Enemy";
    [SerializeField] private LayerMask _maskObs;
    [SerializeField] private WeaponType _projectileType;
    protected float _currentLife;
    protected Rigidbody _rBody;
    protected float _damage = 1;
    protected float _speed = 1;
    protected Vector3 _lastPosition = Vector3.zero;
    protected bool _startCountdown = false;

    public Vector3 LastPositionDirection => (_lastPosition - transform.position).normalized;
    public float LastPositionDistance => Vector3.Distance(transform.position, _lastPosition);
    public WeaponType ProjectileType => _projectileType;
    public Action OnSleep;

    protected private virtual void Awake()
    {
        TryGetComponent(out Rigidbody rBody);
        if (rBody != null) _rBody = rBody;
    }

    protected private virtual void OnEnable()
    {
        UpdateStats();
        _rBody.velocity = Vector3.zero;
        _lastPosition = Vector3.zero;
    }

    protected private virtual void OnDisable()
    {
        UpdateStats();
        _lastPosition = Vector3.zero;
        _startCountdown = false;
        OnSleep?.Invoke();
    }

    protected private virtual void Update()
    {
        if (!_startCountdown) return;

        _currentLife -= Time.deltaTime;

        if (_currentLife <= 0)
            gameObject.SetActive(false);

        if (_lastPosition == Vector3.zero) return;
        RaycastToBack();
    }

    protected private virtual void FixedUpdate()
    {
        if (!_startCountdown) return;

        _rBody.AddForce(transform.forward * _speed, ForceMode.Impulse);

        if (transform.position != Vector3.zero)
            _lastPosition = transform.position;
    }

    protected private virtual void OnTriggerEnter(Collider other)
    {
        if (!_startCountdown) return;

        if (!other.CompareTag(_targetTag)) return;
        other.TryGetComponent(out IDamageable damageble);

        if (damageble == null) return;
        ProjectileHit(damageble);
    }

    public virtual void RaycastToBack()
    {
        // Check if this projectile has ignored anything in the previous frame?
        Debug.DrawLine(transform.position, _lastPosition, Color.yellow);
        RaycastHit rHit;
        if (Physics.Raycast(transform.position, LastPositionDirection, out rHit, LastPositionDistance))
        {
            rHit.transform.TryGetComponent(out Rigidbody rBody);
            if (rBody != null && rBody != this._rBody && rBody.CompareTag(_targetTag))
                ProjectileHit(rBody);
        }
    }

    public virtual void UpdateStats(float damage, float speed, float life)
    {
        _lastPosition = transform.position;
        _currentLife = life;
        _damage = damage;
        _speed = speed;
        _rBody.AddForce(transform.forward * _speed, ForceMode.Force);
        _startCountdown = true;
    }

    public virtual void UpdateStats(float damage, float speed)
    {
        _lastPosition = transform.position;
        _damage = damage;
        _speed = speed;
        _rBody.AddForce(transform.forward * _speed, ForceMode.Force);
        _startCountdown = true;
    }

    public virtual void UpdateStats()
    {
        _lastPosition = transform.position;
        _rBody.AddForce(transform.forward * _speed, ForceMode.Force);
        _startCountdown = true;
    }

    public virtual void ProjectileHit(IDamageable hit)
    {
        hit.AnyDamage(_damage);
        _currentLife = 0;
    }

    public virtual void ProjectileHit(Rigidbody hitRBody)
    {
        hitRBody.TryGetComponent(out IDamageable damageble);

        // check later for stuff that can be "damaged" but is inmortal? like world environment?
        if (damageble == null)
        {
            _currentLife = 0;
            return;
        }

        ProjectileHit(damageble);
    }
}
