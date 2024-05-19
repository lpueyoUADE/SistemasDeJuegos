using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 100)] public float speed = 4;
    private Rigidbody _rBody;

    private Vector3 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        
        _rBody.AddForce(_movement * speed, ForceMode.Acceleration);
    }
}
