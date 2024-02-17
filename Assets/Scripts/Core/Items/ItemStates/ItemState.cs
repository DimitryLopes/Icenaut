using UnityEngine;
using System;

public abstract class ItemState : ScriptableObject, IInteractable
{
    protected Item item;
    protected Action OnInteract;
    protected Action OnStateEnabled;
    protected Action OnStateDisabled;

    public virtual void SetUp(Item item, Action onInteract = null, Action onStateEnabled = null, Action onStateDisabled = null)
    {
        this.item = item;
        OnInteract = onInteract;
        OnStateEnabled = onStateEnabled;
        OnStateDisabled = onStateDisabled;
    }

    public abstract void OnInteractionEnabled();
    public abstract void OnInteractionDisabled();
    
    public void OnStateEnter()
    {
        OnStateEnabled?.Invoke();
    }

    public void OnStateExit()
    {
        OnStateDisabled?.Invoke();
    }

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}