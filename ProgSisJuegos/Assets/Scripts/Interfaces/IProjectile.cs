using UnityEngine;

public interface IProjectile
{
    void UpdateStats(float damage, float speed);
    void ProjectileHit(IDamageable hit);
    void ProjectileHit(Rigidbody hitRBody);
}
