using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGeneralSoundsData", menuName = "Databases/Sounds/General")]
public class GeneralSoundsDatabase : ScriptableObject
{
    [SerializeField] private List<AudioClip> _projectileHitShipSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _projectileHitShieldSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _shipExplosionSounds = new List<AudioClip>();

    public AudioClip ProjectileHittingShip => _projectileHitShipSounds[Random.Range(0, _projectileHitShipSounds.Count)];
    public AudioClip ProjectileHittingShield => _projectileHitShieldSounds[Random.Range(0, _projectileHitShieldSounds.Count)];
    public AudioClip ShipExplosions => _shipExplosionSounds[Random.Range(0, _shipExplosionSounds.Count)];
}
