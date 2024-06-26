using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    public static UserSettings instance;
    public ShipBase playership;

    public static UserSettings Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
            instance = this;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
