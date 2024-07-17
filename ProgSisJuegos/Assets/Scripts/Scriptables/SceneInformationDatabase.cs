using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneInformationData", menuName = "Databases/Scenes/SceneInformation")]
public class SceneInformationDatabase : ScriptableObject
{
    [SerializeField] private string _sceneNameToUser;
    [SerializeField] private string _sceneName;
    [SerializeField] private Sprite _splashImage;
    [SerializeField] private List<ShipDatabase> _availableShips = new List<ShipDatabase>();
    [SerializeField] private AudioClip _music;

    public string SceneNameToUser => _sceneNameToUser;
    public string SceneName => _sceneName;
    public Sprite SceneSplashImage => _splashImage;
    public List<ShipDatabase> SceneShips => _availableShips;
    public AudioClip SceneMusic => _music;
}
