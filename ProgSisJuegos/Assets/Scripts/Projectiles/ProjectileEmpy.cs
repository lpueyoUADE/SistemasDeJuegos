using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmpy : ProjectileBase
{
    private void Awake()
    {
        Debug.Log("This projectile is only for testing purposes.");
        Destroy(gameObject);
    }
}
