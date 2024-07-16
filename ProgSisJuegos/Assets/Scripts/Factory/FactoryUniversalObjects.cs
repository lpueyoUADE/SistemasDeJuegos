using System.Collections.Generic;
using UnityEngine;

public class FactoryUniversalObjects : MonoBehaviour
{
    private static List<UniversalPooleableObject> _objects = new List<UniversalPooleableObject>();

    public static void UpdateAvailableObjects(List<UniversalPooleableObject> objects)
    {
        _objects.Clear();
        string message = "Factory Universal Objects: \n";
        foreach (UniversalPooleableObject theObject in objects) message += $"{theObject.name}, ";
        _objects = objects;
        Debug.Log($"{message} Initialized {_objects.Count} items.");
    }

    public static UniversalPooleableObject GeneratePooleableObject(UniversalPoolObjectType type)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i].objectType == type) return Instantiate(_objects[i]);
        }

        Debug.Log($"Factory: No universal pooleable object of type {type} was found.");
        return null;
    }
}
