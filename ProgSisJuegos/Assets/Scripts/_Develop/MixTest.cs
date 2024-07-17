using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixTest : MonoBehaviour
{
    public ShipBase playerPrefab;

    private void Awake()
    {
        UserSettings.UpdatePlayerShip(playerPrefab);
    }
}
