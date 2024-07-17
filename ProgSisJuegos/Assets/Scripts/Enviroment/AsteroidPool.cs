using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    private List<GameObject> asteroids;

    private void Awake()
    {
        asteroids = new List<GameObject>();
    }

    public void AddToPool(ObstacleScript asteroid)
    {
        if (asteroids == null)
        {
            asteroids = new List<GameObject>();
        }
        asteroids.Add(asteroid.gameObject);

    }

    public GameObject GetFromPool()
    {
        if (asteroids != null && asteroids.Count > 0)
        {
            GameObject asteroid = asteroids[asteroids.Count - 1];
            asteroids.Remove(asteroid);
            return asteroid;
        }
        else
        {
            return null;
        }
        
    }

    public bool IsPoolEmpty()
    {
        if (asteroids != null && asteroids.Count > 0)
        {            
            return false;
        }
        else
        {
            return true;
        }
    }
}
