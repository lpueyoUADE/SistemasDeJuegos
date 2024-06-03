using System;

public interface IDamageable
{
    void AnyDamage(float amount);
    void OnDeath();

    public event Action OnDestroy;
}
