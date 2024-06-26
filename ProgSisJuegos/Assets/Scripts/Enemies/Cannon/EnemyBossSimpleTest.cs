using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBossSimpleTest : MonoBehaviour, IDamageable
{
    public float life = 100;
    public TextMeshPro lifetext;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PlayerProjectile")) return;
    }

    public void AnyDamage(float amount)
    {
        life -= amount;
        lifetext.text = $"{life}";
    }

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifetext.text = $"{life}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
