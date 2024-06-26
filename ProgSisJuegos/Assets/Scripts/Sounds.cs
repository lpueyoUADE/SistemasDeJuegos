using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sounds
{
    private static GeneralSoundsDatabase _sounds;
    public static GeneralSoundsDatabase SoundsDatabase => _sounds;

    public static void UpdateDatabase(GeneralSoundsDatabase database)
    {
        _sounds = database;
    }

}
