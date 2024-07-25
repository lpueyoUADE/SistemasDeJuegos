using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderRTShipSelector : MonoBehaviour
{
    [SerializeField] private RenderTexture _renderTexture;

    private RawImage _sprite;
    private Material _material;

    private void Awake()
    {
        _sprite = GetComponent<RawImage>();
        
    }

    private void Start()
    {
        _material = _sprite.material;
    }

    private void FixedUpdate()
    {
        if (_material != null) _material.SetTexture("_RenderTexture", _renderTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}
