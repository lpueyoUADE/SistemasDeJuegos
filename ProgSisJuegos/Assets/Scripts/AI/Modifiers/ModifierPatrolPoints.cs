using System.Collections.Generic;
using UnityEngine;

public class ModifierPatrolPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private float _tolerance = 1;
    private int _index;

    public List<Transform> PatrolPoints => _points;
    public int PatrolPointsCount => _points.Count;
    public int Index => _index;
    public float Tolerance => _tolerance;

    public void UpdateIndex(int newIndex)
    {
        _index = newIndex;
    }

    public Vector3 GetPatrolPoint(int index)
    {
        _index = index;
        return _points[index].position;
    }

    public Vector3 GetCurrentPatrolPoint()
    {
        return _points[_index].position;
    }
}
