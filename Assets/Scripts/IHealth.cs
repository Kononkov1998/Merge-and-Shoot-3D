using System;

public interface IHealth : IDamageReceiver
{
    float Current { get; }
    float Max { get; }
    event Action Changed;
}

public interface IDamageReceiver
{
    void TakeDamage(float damage);
}