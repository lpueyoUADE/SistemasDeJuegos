using System;
using UnityEngine;

public static class GameManagerEvents
{
    private static GameObject _playerPrefab;
    public static GameObject PlayerPrefab => _playerPrefab;

    public static void UpdatePlayerShip(GameObject playerPrefab)
    {
        _playerPrefab = playerPrefab;
    }

    // General game events
    public static Action OnGameInitialized;
    public static Action OnGamePaused;
    public static Action OnGameEnded;

    // Scenes
    public static Action OnSceneChange;

    // InGame evenets
    public static Action<float> OnEnemyDestroyed;
}
