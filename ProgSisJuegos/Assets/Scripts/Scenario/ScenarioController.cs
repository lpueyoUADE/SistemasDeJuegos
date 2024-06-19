using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Terrain> terrainList;

    private float distanceAccumulated;

    // Start is called before the first frame update
    void Start()
    {
        distanceAccumulated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distanceAccumulated += speed * Time.deltaTime;

        foreach (var terrain in terrainList)
        {
            terrain.transform.position += Vector3.back * speed * Time.deltaTime;
        }

        if(distanceAccumulated >= terrainList[0].GetComponent<TerrainCollider>().bounds.size.z)
        {
            Terrain last = terrainList[terrainList.Count -1];
            float lastSize = last.GetComponent<TerrainCollider>().bounds.size.z;

            Terrain current = terrainList[0];

            current.transform.position = new Vector3(
                current.transform.position.x,
                current.transform.position.y,
                last.transform.position.z + lastSize
            );




            terrainList.Add(current);
            terrainList.RemoveAt(0);
            distanceAccumulated = 0;
        }
    }
}
