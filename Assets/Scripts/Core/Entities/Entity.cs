using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public abstract void OnDamageTaken(float damage);
}
