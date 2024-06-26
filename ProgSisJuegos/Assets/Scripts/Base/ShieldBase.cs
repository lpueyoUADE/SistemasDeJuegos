using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBase : MonoBehaviour
{
    public Color shieldColor;
    public float shieldSpeed;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        UpdateShieldStats(shieldColor, shieldSpeed);
    }

    private void OnDisable()
    {
        _meshRenderer.material.SetFloat("_Speed", 0);
    }

    private void OnEnable()
    {
        _meshRenderer.material.SetFloat("_Speed", shieldSpeed);
    }

    public void UpdateShieldStats(Color newColor, float speed)
    {
        shieldColor = newColor;
        shieldSpeed = speed;

        _meshRenderer.material.SetColor("_shieldcolor", shieldColor);
        _meshRenderer.material.SetFloat("_Speed", shieldSpeed);
    }
}
