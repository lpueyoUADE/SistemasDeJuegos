using System;
using UnityEngine;

public static class GameManagerEvents
{
    // General game events
    public static Action OnGameInitialized;
    public static Action OnGamePaused;
    public static Action OnGameEnded;

    // Scenes
    public static Action OnSceneChange;

    // InGame events
    //public delegate GameObject CreateEnemy(ShipDatabase ship, Vector3 Xposition);
    public static Action<ShipDatabase, Vector3> CreateEnemy;
    public static Action<float> OnEnemyDestroyed;
}
