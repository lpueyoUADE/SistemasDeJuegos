using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private float _life = 5;
    [SerializeField] private WeaponType _projectileType;
    private float _currentLife;
    private Rigidbody _rBody;

    private float _damage;
    private float _speed;

    public WeaponType ProjectileType => _projectileType;
    public Action OnSleep;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _currentLife = _life;
    }

    void Update()
    {
        _currentLife -= Time.deltaTime;

        if (_currentLife <= 0)
            gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        _rBody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        _rBody.velocity= Vector3.zero;
        _currentLife = _life;
    }

    private void OnDisable()
    {
        OnSleep?.Invoke();
    }

    public void UpdateStats(float damage, float speed, float life = 5)
    {
        _life = life;
        _damage = damage;
        _speed = speed;
    }

}
