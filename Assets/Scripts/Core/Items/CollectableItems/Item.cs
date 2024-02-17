using UnityEngine;

public class Item : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected ItemData itemData;

    public ItemData Data => itemData;

    public bool Active => gameObject.activeSelf;

    public bool IsActive { get ; set ; }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        IsActive = true;
        EventManager.OnItemActivated.Invoke(this);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        IsActive = false;
        EventManager.OnItemDeactivated.Invoke(this);
    }

    public virtual void OnPickUp()
    {
        Debug.Log("Got item: " + gameObject.name);
        ItemManager.Instance.ChangeItemAmount(Data.Type, 1);
        Deactivate();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            OnPickUp();
        }
    }
}
