using System.Collections.Generic;
using UnityEngine;

public enum UniversalPoolObjectType
{
    Audio, Effect
}

[CreateAssetMenu(fileName = "NewUniversalPooleableObjectData", menuName = "Databases/Universal Pooleable Object")]
public class UniversalPooleableObjectsDatabase : ScriptableObject
{
    [SerializeField] private UniversalPooleableObject _pooleableSoundPrefab;

    public UniversalPooleableObject PooleableSound => _pooleableSoundPrefab;
    public List<UniversalPooleableObject> PooleableObjects => new List<UniversalPooleableObject> 
    { 
        _pooleableSoundPrefab,
    };
}
