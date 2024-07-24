using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShaderPPFog : MonoBehaviour 
{
    public Shader _shader;
    public Color _fogColor = Color.grey;
    [Range(0, 1)]
    [SerializeField] private float _intensity = 0.35f;
    private float _oldIntensity;

    private Material _mat;

    private void Start()
    {
        _mat = new Material(_shader);
        _mat.SetColor("_FogColor", _fogColor);
        _oldIntensity = _intensity;
    }

    private void Update()
    {
        if (_oldIntensity != _intensity) SetValues();
    }

    private void SetValues()
    {
        _mat.SetFloat("_Intensity", _intensity);
        _oldIntensity = _intensity;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _mat);
    }
}
