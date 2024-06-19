using UnityEngine;

public interface IProjectile
{
    void UpdateStats();
    void UpdateStats(float damage, float speed);
    void UpdateStats(float damage, float speed, float life);
    void ProjectileHit(IDamageable hit);
    void ProjectileHit(Rigidbody hitRBody);
}
