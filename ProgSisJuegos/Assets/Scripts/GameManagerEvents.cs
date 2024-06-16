using System;

public static class GameManagerEvents
{
    public static Action OnGameInitialized;
    public static Action OnGamePaused;
    public static Action OnGameEnded;

    public static Action OnSceneChange;
}
