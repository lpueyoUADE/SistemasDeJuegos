using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneInformationData", menuName = "Databases/Scenes/SceneInformation")]
public class SceneInformationDatabase : ScriptableObject
{
    [SerializeField] string _sceneNameToUser;
    [SerializeField] string _sceneName;
    [SerializeField] Sprite _splashImage;

    public string SceneNameToUser => _sceneNameToUser;
    public string SceneName => _sceneName;
    public Sprite SplashImage => _splashImage;
}
