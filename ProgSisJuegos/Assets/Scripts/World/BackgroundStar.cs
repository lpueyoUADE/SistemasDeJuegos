using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStar : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();

    [Header("Getting close to the camera")]
    [SerializeField] private float _minZBound = -20;

    [Header("Far away from the camera")]
    [SerializeField] private Vector3 _maxBounds = Vector3.zero;

    [SerializeField, Range(1, 50)] private float _minSpeed = 23;
    [SerializeField, Range(1, 50)] private float _maxSpeed = 45;

    private SpriteRenderer _spr;
    public float _currentSpeed;
    public Vector3 _currentMinBounds;
    public Vector3 _currentMaxBounds;

    private void Awake()
    {
        _spr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        RestartStar();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.deltaTime * _currentSpeed);

        if (transform.position.z < _minZBound)
            RestartStar();
    }

    private void RestartStar()
    {
        _spr.sprite = _spriteList[Random.Range(0, _spriteList.Count)];
        float newMinSpeed = Random.Range(0, _minSpeed);
        float newMaxSpeed = Random.Range(newMinSpeed, _maxSpeed);

        transform.position = new Vector3(Random.Range(-_maxBounds.x, _maxBounds.x), Random.Range(-_maxBounds.y, _maxBounds.y), _maxBounds.z);
        _currentSpeed = Random.Range(newMinSpeed, newMaxSpeed);
    }
}
