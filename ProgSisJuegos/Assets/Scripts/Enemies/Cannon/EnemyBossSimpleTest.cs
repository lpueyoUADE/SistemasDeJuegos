using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBossSimpleTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float life = 100;
    private float currentLife;
    [SerializeField] TextMeshPro lifetext;
    private float _destination;
    [SerializeField] private UnityEngine.UI.Image healthBar;



    private void OnCollisionEnter(Collision collision)
    {
        //if (!collision.gameObject.CompareTag("PlayerProjectile")) return;
    }

    public void AnyDamage(float amount)
    {
        if (currentLife > 0)
        {
            currentLife -= amount;
            healthBar.fillAmount = currentLife / life;
            
            lifetext.text = $"{currentLife}";            
        }
        else
        {
            OnDeath();
        }
        
    }

    public void OnDeath()
    {
        UIEvents.OnGameEnded(true);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLife = life;
        //lifetext.text = $"{life}";       
        _destination = transform.position.z - 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, _destination), 0.5f);
        }
    }
   
}
