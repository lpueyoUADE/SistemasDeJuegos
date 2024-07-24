using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShaderPPLowLife : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [SerializeField, Range(0.35f, 0.5f)] private float _pulseMinIntegrity = 0.35f;
    [SerializeField, Range(0.1f, 25)] private float _pulseSpeed = 0.5f;
    [SerializeField, Range(0, 1)] private float _pulseMaxIntensity = 1;
    [SerializeField] private Color _pulseColor = Color.red;
    private Material _material;

    private void Awake()
    {
        UIEvents.OnPlayerHPUpdate += UpdateIntensity;
    }

    private void OnDestroy()
    {
        UIEvents.OnPlayerHPUpdate -= UpdateIntensity;
    }

    private void Start()
    {
        _material = new Material(_shader);
        _material.SetColor("_Color", _pulseColor);
        _material.SetFloat("_PulseSpeed", _pulseSpeed);
        _material.SetFloat("_PulseMaxIntensity", _pulseMaxIntensity);

        _material.SetFloat("_PulseIntensity", 0);
        _material.SetFloat("_BrokenOverlayIntensity", 0);
    }

    private void UpdateIntensity(float currentIntegrity, float originalIntegrity)
    {
        float integrityValue = (currentIntegrity / originalIntegrity) * 1;
        float remaining = 1 - integrityValue;
        float incrementalValue = remaining * _pulseMaxIntensity;

        _material.SetFloat("_BrokenOverlayIntensity", remaining);

        if (integrityValue <= _pulseMinIntegrity)
        {
            _material.SetFloat("_PulseSpeed", _pulseSpeed  + incrementalValue * 2);
            _material.SetFloat("_PulseIntensity", incrementalValue);
        }
        else _material.SetFloat("_PulseIntensity", 0);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}
