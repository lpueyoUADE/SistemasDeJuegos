using AmplifyShaderEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBase : MonoBehaviour
{
    public Color shieldColor;
    public float shieldSpeed;

    private float _shieldInitialTime;
    private MeshRenderer _meshRenderer;
    private Material _shieldShader;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        UpdateShieldStats(shieldColor, shieldSpeed, 0);

        TryGetComponent(out Renderer renderer);
        if (renderer != null) _shieldShader = renderer.GetComponent<Renderer>().material;
    }

    private void OnDisable()
    {
        _meshRenderer.material.SetFloat("_Speed", 0);
    }

    private void OnEnable()
    {
        _meshRenderer.material.SetFloat("_Speed", shieldSpeed);
    }

    public void UpdateShieldColorIntegrity(Color newColor)
    {
        _shieldShader.SetColor("_ShipIntegrityColor", newColor);
    }

    public void UpdateShieldStats(Color newColor, float speed, float time)
    {
        shieldColor = newColor;
        shieldSpeed = speed;
        _shieldInitialTime = time;

        _meshRenderer.material.SetColor("_ShieldColor", shieldColor);
        _meshRenderer.material.SetFloat("_Speed", shieldSpeed);
    }

    public void UpdateShieldTime(float newTime)
    {
        _meshRenderer.material.SetFloat("_ShieldLerpBorder", newTime / _shieldInitialTime * 1);
    }
}
