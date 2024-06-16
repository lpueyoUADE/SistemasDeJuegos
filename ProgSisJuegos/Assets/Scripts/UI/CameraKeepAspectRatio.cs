using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeepAspectRatio : MonoBehaviour
{
    [SerializeField] private float _targetAR = 16.0f / 9.0f;
    [SerializeField] private float _windowAR = (float)Screen.height / (float)Screen.width;
    [SerializeField] private float _scaleFactor;

    private void FixedUpdate()
    {
        if (_windowAR >= _targetAR)
        {
            _scaleFactor = _windowAR / _targetAR;
            Camera.main.orthographicSize = Camera.main.orthographicSize * _scaleFactor;
        }
    }
}
