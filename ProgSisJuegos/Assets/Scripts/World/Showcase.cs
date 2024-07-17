using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Showcase : MonoBehaviour
{
    public TextMeshProUGUI uitext;
    public List<GameObject> objectsList = new List<GameObject>();
    public List<string> namesList = new List<string>();
    public List<Vector3> offsets = new List<Vector3>();
    public int index = 0;

    private Vector3 _direction;

    public float axis => Input.GetAxisRaw("Horizontal");

    private void Start()
    {
        uitext.text = namesList[0];
        _direction = objectsList[0].transform.position + offsets[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (index == 0) index = objectsList.Count - 1;
            else index--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (index >= objectsList.Count - 1) index = 0;
            else index++;
        }

        uitext.text = namesList[index];
        _direction = objectsList[index].transform.position + offsets[index];
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.MoveTowards(transform.position, _direction, 1);
        transform.position = direction;
    }
}
