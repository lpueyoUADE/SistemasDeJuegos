using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    private float _speed;
    private Vector3 _forward;
    private Vector3 _boundaries;
    private Rigidbody _rBody;

    public Action<ObstacleObject> OnBoundReached;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
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

    public void UpdateSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }


    private void FixedUpdate()
    {
        _rBody.AddForce(_forward * _speed, ForceMode.Impulse);

        if (transform.position.z <= _boundaries.z) BoundReached();
    }

    private void BoundReached()
    {
        OnBoundReached?.Invoke(this);
    }
}
