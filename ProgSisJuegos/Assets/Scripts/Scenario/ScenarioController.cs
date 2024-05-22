using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector3 selectedAxis;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += -Vector3.forward * speed * Time.deltaTime;
    }
}
