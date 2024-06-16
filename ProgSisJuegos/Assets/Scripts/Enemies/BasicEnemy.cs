using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    private Transform _targetLocation;

    public override void Move(Vector3 direction, float speed, ForceMode type = ForceMode.Acceleration)
    {
        // movimiento tiene que llamarse aca
        base.Move(direction, speed, type);
    }

    public virtual void UpdateTargetLocation(Transform newTargetLocation)
    {
        _targetLocation = newTargetLocation;
    }

    // 


    /*
    [SerializeField ]private float positionCheckDelay = 5;
    private float positionCheckTimer = 0;
    private bool moveRight = false;

    
    // Update is called once per frame
    protected override void Update()
    {         
        if(positionCheckTimer < positionCheckDelay)
        {
            positionCheckTimer += Time.deltaTime;
        }
        else
        {
            CheckPlayerPosition();
            positionCheckTimer = 0;
        }

        if (moveRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        base.Update();
    }

    
    private void MoveLeft()
    {
        this.transform.position += -transform.right * _data.Speed * Time.deltaTime;
    }

    private void MoveRight()
    {
        this.transform.position += transform.right * _data.Speed * Time.deltaTime;
    }

    private void CheckPlayerPosition()
    {
        if ((this.gameObject.transform.position.x - _player.transform.position.x) < 0)
        {
            moveRight= true;
        }
        else if ((this.gameObject.transform.position.x - _player.transform.position.x) > 0)
        {
            moveRight = false;
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InvisibleWall")
        {
            moveRight = !moveRight;
        }
    }
    */
}
