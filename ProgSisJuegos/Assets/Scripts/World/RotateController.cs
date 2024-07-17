using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X, Y, Z
}

public class RotateController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Axis axis;
    private Vector3 selectedAxis;

    void Start()
    {
        switch (axis){
            case Axis.X:
                selectedAxis = Vector3.right; break;

            case Axis.Y:
                selectedAxis = Vector3.up; break;

            case Axis.Z:
                selectedAxis = Vector3.down; break;
        }
    }

    void FixedUpdate()
    {
        this.transform.RotateAround(this.transform.position, selectedAxis, speed * Time.deltaTime);
    }
}
