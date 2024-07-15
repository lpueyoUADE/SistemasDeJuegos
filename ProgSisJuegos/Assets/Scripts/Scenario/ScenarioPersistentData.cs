using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioPersistentData : MonoBehaviour
{
    private static ScenarioPersistentData _instance;
    private static SceneInformationDatabase _sceneData;

    public static ScenarioPersistentData Instance => _instance;
    public static SceneInformationDatabase SceneData => _sceneData;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            _instance = this;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScenarioData(SceneInformationDatabase newData)
    {
        _sceneData = newData;
    }
}
