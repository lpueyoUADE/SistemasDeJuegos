using UnityEngine;

[CreateAssetMenu(fileName = "NewAIBehaviourData", menuName = "Databases/AI Behaviour Data")]
public class IABehaviourDatabase : ScriptableObject
{
    [Header("Idle")]
    [SerializeField] private float _idleProbability = 1;
    [SerializeField, Range(0.1f, 10f)] private float _minIdleTime = .75f;
    [SerializeField, Range(0.1f, 10f)] private float _maxIdleTime = 2;

    [Header("Death")]
    [SerializeField, Range(0.1f, 2)] private float _deathTime = 1;

    public float IdleProbability => _idleProbability;
    public float IdleTimeMin => _minIdleTime;
    public float IdleTimeMax => _maxIdleTime;

    public float DeathTime => _deathTime;
}
