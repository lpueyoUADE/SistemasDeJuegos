using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamesScenes
{
    MainMenu, TestScene
}

[CreateAssetMenu(fileName = "NewScenesData", menuName = "Databases/Scenes")]
public class ScenesDatabase : ScriptableObject
{
    [SerializeField] private string _mainMenuScene;
    [SerializeField] private string _testScene;
    [SerializeField] private List<string> _gamePlayScenes = new List<string>();

    public string SceneMainMenu => _mainMenuScene;
    public string SceneTestScene => _testScene;
}
