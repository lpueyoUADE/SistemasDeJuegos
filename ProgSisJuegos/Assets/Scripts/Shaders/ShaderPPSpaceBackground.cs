using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShaderPPSpaceBackground : MonoBehaviour
{
    public Shader _shader;
    private Material _mat;

    private void Start()
    {
        _mat = new Material(_shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _mat);
    }
}
