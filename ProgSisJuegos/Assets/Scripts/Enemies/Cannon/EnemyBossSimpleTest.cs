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
        //if (!collision.gameObject.CompareTag("PlayerProjectile")) return;
    }

    public void AnyDamage(float amount)
    {
        if (life > 0)
        {
            life -= amount;
            lifetext.text = $"{life}";
        }
        else
        {
            OnDeath();
        }
        
    }

    public void OnDeath()
    {
        UIEvents.OnPlayerWin();
        Destroy(this.gameObject);
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
