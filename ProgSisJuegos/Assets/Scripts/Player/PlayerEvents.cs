using System;
using System.Collections;
using System.Collections.Generic;

public static class PlayerEvents
{
    public static Action<PlayerController> OnPlayerSpawn;
    public static Action<float> OnPlayerHPUpdate;
    public static Action OnPlayerDeath;
    public static Action<float> OnPlayerMaxLifeUpdate;
}
