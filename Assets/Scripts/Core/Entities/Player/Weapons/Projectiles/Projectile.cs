using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifeSpan;

    private float damage;
    private float timer;

    public bool Active => gameObject.activeSelf;

    public void Shoot(float damage, Vector3 direction)
    {
        gameObject.SetActive(true);
        rigidbody.velocity = direction * speed;
        this.damage = damage;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeSpan)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == targetTag)
        {
            Entity entity = collider.gameObject.GetComponent<Entity>();
            entity.OnDamageTaken(damage);
        }
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        timer = 0;
    }
}