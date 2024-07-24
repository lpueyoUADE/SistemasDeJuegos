using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioPersistentData : MonoBehaviour
{
    public SceneInformationDatabase overrideScenarioData;
    [SerializeField] private UniversalPooleableObjectsDatabase _genericPooleableObjects;
    private static ScenarioPersistentData _instance;
    private static SceneInformationDatabase _sceneData;

    public static ScenarioPersistentData Instance => _instance;
    public static SceneInformationDatabase SceneData => _sceneData;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }

        _instance = this;
        FactoryUniversalObjects.UpdateAvailableObjects(_genericPooleableObjects.PooleableObjects);
        DontDestroyOnLoad(gameObject);

        if (overrideScenarioData != null) UpdateScenarioData(overrideScenarioData);
    }

    public void UpdateScenarioData(SceneInformationDatabase newData)
    {
        _sceneData = newData;
    }

    public UniversalPooleableObjectsDatabase GetUniversalPooleableObjects()
    {
        return _genericPooleableObjects;
    }
}
