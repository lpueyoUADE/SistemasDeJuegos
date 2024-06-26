using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseCameraController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);

        rb.velocity = direction;
    }
}
