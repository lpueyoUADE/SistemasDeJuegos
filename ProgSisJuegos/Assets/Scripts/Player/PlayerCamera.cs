using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float followSpeed = 3;
    public Vector3 offset;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;

        transform.position = Vector3.LerpUnclamped(
            transform.position, 
            _player.transform.position + offset, 
            Time.deltaTime);
    }
}
