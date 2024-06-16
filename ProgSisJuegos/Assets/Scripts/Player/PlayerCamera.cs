using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float followSpeed = 3;
    public Vector3 offset;
    public PlayerController _player;

    void FixedUpdate()
    {
        if (_player == null) return;

        transform.position = Vector3.LerpUnclamped(
            transform.position, 
            _player.transform.position + offset, 
            Time.deltaTime);
    }
}
