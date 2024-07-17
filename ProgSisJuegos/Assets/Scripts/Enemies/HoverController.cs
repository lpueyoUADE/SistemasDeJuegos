using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    [Header("Attributes")]
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private float seed;
    void Start()
    {
        seed = Random.Range(0, 2);
        frequency += Random.Range(frequency * -0.1f, frequency * 0.1f);
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * frequency + seed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
