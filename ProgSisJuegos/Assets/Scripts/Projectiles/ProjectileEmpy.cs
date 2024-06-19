using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmpy : ProjectileBase
{
    private protected override void Awake()
    {
        base.Awake();
        Debug.Log("This projectile is only for testing purposes.");
        this.gameObject.SetActive(false);
    }
}
