using UnityEngine;

public class StatePatrolGeneric<T> : StateBase<T>
{
    private ShipBase _controller;
    private ModifierPatrolPoints _patrolPoints;

    private int _index;
    private bool _isReversePatrol;

    public StatePatrolGeneric(ShipBase controller, ModifierPatrolPoints patrolPoints)
    {
        _controller = controller;
        _patrolPoints = patrolPoints;
    }

    public override void Execute()
    {
        Vector3 dir = _patrolPoints.GetPatrolPoint(_index) - _controller.transform.position;
        _controller?.Move(dir.normalized);

        if (dir.magnitude <= _patrolPoints.Tolerance)
        {
            if (!_isReversePatrol)
            {
                if (_index == _patrolPoints.PatrolPointsCount - 1)
                {
                    _isReversePatrol = true;
                    _index = _patrolPoints.PatrolPointsCount - 2;
                    return;
                }

                _index++;
            }

            else
            {
                if (_index == 0)
                {
                    _isReversePatrol = false;
                    _index = 1;
                    return;
                }

                _index--;
            }
        }
    }

    public override void Sleep()
    {
        _patrolPoints.UpdateIndex(_index);
    }
}
