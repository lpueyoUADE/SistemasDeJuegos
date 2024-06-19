using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGreenCrast : ProjectileBase
{
    [SerializeField] private int _maxHits = 3;
    private int _hitsLeft;
    private List<IDamageable> _lastHit = new List<IDamageable>();

    public override void UpdateStats(float damage, float speed, float life)
    {
        _lastHit.Clear();
        _hitsLeft = _maxHits;
        base.UpdateStats(damage, speed, life);
    }

    public override void ProjectileHit(IDamageable hit)
    {
        if (_lastHit.Contains(hit)) return;

        _lastHit.Add(hit);
        hit.AnyDamage(_damage);

        if (_hitsLeft <= 0) _currentLife = 0;
        _hitsLeft--;
    }

    public override void ProjectileHit(Rigidbody hitRBody)
    {
        hitRBody.TryGetComponent(out IDamageable damageble);

        if (_lastHit.Contains(damageble)) return;

        // check later for stuff that can be "damaged" but is inmortal? like world environment?
        if (damageble == null)
        {
            if (_hitsLeft <= 0) _currentLife = 0;
            return;
        }

        ProjectileHit(damageble);
    }
    
}
