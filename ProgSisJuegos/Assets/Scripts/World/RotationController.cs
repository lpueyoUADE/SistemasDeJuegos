using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Variables públicas para controlar la velocidad de rotación en cada eje
    [SerializeField] float rotationSpeedX = 10f;
    [SerializeField] float rotationSpeedY = 10f;
    [SerializeField] float rotationSpeedZ = 10f;
    [SerializeField] float multiplier = 1f;
    private void Start()
    {
        rotationSpeedX += Random.Range(-rotationSpeedX * 0.1f, +rotationSpeedX * 0.1f);
        rotationSpeedY += Random.Range(-rotationSpeedY * 0.1f, +rotationSpeedY * 0.1f);
        rotationSpeedZ += Random.Range(-rotationSpeedZ * 0.1f, +rotationSpeedZ * 0.1f);
    }

    void Update()
    {
        // Obtener la entrada del usuario
        float rotationX = rotationSpeedX * multiplier * Time.deltaTime;
        float rotationY = rotationSpeedY * multiplier * Time.deltaTime;
        float rotationZ = rotationSpeedZ * multiplier * Time.deltaTime;

        // Aplicar la rotación al objeto
        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}