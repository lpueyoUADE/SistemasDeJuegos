using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Axis axis;
    public enum Axis
    {
        X,
        Y,
        Z
    }

    private Vector3 selectedAxis;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position, selectedAxis, speed * Time.deltaTime);
    }
}
