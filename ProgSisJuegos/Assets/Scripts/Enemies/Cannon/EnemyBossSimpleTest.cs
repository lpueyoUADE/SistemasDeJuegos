using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBossSimpleTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float _life = 100;
    [SerializeField] private UnityEngine.UI.Image _healthBar;

    private float _currentLife;
    private float _destination;

    private void OnCollisionEnter(Collision collision)
    {
        //if (!collision.gameObject.CompareTag("PlayerProjectile")) return;
    }

    public void AnyDamage(float amount)
    {
        if (_currentLife > 0)
        {
            _currentLife -= amount;
            _healthBar.fillAmount = _currentLife / _life;
        }
        else
        {
            OnDeath();
        }
        
    }

    public void OnDeath()
    {
        GameManagerEvents.OnGameEnded();
        Destroy(this.gameObject);
    }

    void Start()
    {
        _currentLife = _life;   
        _destination = transform.position.z - 20;
    }

    void Update()
    {
        if (transform.position.y > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, _destination), 0.5f);
        }
    }
   
}
