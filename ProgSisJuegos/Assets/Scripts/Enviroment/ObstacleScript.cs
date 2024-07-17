using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private float _destination;
    [SerializeField] ObstacleDatabase data;

    private AsteroidPool _pool;
    private float currentLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        _destination = transform.position.z * -100;

    }

    private void OnEnable()
    {
        currentLifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= data.Lifetime)
        {
            _pool.AddToPool(this);
            this.gameObject.SetActive(false);
        }

        if (transform.position.z != _destination)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, _destination), data.Speed);
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().AnyDamage(data.Damage);
            print("player hit by asteroid");
            _pool.AddToPool(this);
            this.gameObject.SetActive(false);
        }
        else
        {
            _pool.AddToPool(this);
            this.gameObject.SetActive(false);
        }
    }

    public void AssignPool(AsteroidPool asteroidPool)
    {
        _pool = asteroidPool;
    }
}
