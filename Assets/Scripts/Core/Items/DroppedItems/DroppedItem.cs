using UnityEngine;

public class DroppedItem : Item
{
    [SerializeField]
    private Rigidbody rb;

    private float timer;

    public new DroppedItemData Data => (DroppedItemData)itemData;

    public void Drop(Vector3 from)
    {
        transform.position = from;
        Vector3 randomDirection = Random.insideUnitSphere;
        rb.AddForce(randomDirection * Data.ExplosionForce, ForceMode.Impulse);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= Data.Duration)
        {
            Deactivate();
        }
    }
}
