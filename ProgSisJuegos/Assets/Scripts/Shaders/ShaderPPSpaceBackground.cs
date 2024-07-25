using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShaderPPSpaceBackground : MonoBehaviour
{
    public Shader _shader;
    public Texture _spaceTexture;
    public Texture _starsTexture;

    private Material _mat;

    private void Start()
    {
        _mat = new Material(_shader);
        _mat.SetTexture("_SpaceColored", _spaceTexture);
        _mat.SetTexture("_SpaceOnlyStars", _starsTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _mat);
    }
}
