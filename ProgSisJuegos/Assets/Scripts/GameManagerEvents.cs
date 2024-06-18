using System;

public static class GameManagerEvents
{
    // General game events
    public static Action OnGameInitialized;
    public static Action OnGamePaused;
    public static Action OnGameEnded;

    // Scenes
    public static Action OnSceneChange;

    // InGame evenets
    public static Action<float> OnEnemyDestroyed;
}
