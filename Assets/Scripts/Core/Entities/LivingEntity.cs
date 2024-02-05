using UnityEngine;

public abstract class LivingEntity : Entity
{
    [SerializeField]
    protected float maxHealth;

    protected float health;
    protected bool isAlive = true;

    public virtual void Start()
    {
        health = maxHealth;
        Respawn();
    }

    public override void OnDamageTaken(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public abstract void Die();

    public virtual void Respawn()
    {
        isAlive = true;
        health = maxHealth;
    }
}
