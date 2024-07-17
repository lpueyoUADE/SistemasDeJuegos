using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _maxSpeed = 20;
    private float _speed;
    
    private Vector3 _forward;
    private Vector3 _boundaries;
    private Rigidbody _rBody;

    public Action<ObstacleObject> OnObstacleDisable;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rBody.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        _rBody.velocity = Vector3.zero;
    }

    public void UpdateForward(Vector3 newForward)
    {
        _forward = newForward;
    }

    public void UpdateBoundaries(Vector3 newPosition)
    {
        _boundaries = newPosition;
    }

    public void UpdateSpeeds(float newSpeed, float newMaxSpeed)
    {
        _speed = newSpeed;
        _maxSpeed = newMaxSpeed;
    }


    private void FixedUpdate()
    {
        float speed = Vector3.Magnitude(_rBody.velocity);
        if (speed > _maxSpeed)
        {
            float brakeSpeed = speed - _maxSpeed;
            Vector3 normalisedVelocity = _rBody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
            _rBody.AddForce(-brakeVelocity);
        }

        else
            _rBody.AddForce(_forward * _speed, ForceMode.Impulse);

        if (transform.position.z <= _boundaries.z) OnObstacleDisable?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent(out IDamageable damageable);
        if (collision == null) return;
        damageable?.AnyDamage(_damage);
        AsteroidHit();
    }

    private void OnTriggerEnter(Collider other)
    {
        AsteroidHit();
    }

    private void AsteroidHit()
    {
        var audio = Pool.CreateUniversalObject(UniversalPoolObjectType.Audio);
        audio.transform.position = transform.position;
        audio.UpdateAudioAndPlay(Sounds.SoundsDatabase.ObstacleHit);
        OnObstacleDisable?.Invoke(this);
    }

    public void AnyDamage(float amount)
    {
        AsteroidHit();
    }

    public void OnDeath()
    {
        return;
    }
}
