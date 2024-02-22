using UnityEngine;

public abstract class LivingEntity : Entity
{
    [SerializeField]
    protected float maxHealth;

    protected float health;
    protected bool isAlive = true;

    public virtual void Start()
    {
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

    public virtual void OnHealthRecovered(float recovery)
    {
        health += recovery;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public abstract void Die();

    public virtual void Respawn()
    {
        isAlive = true;
        health = maxHealth;
    }
}
