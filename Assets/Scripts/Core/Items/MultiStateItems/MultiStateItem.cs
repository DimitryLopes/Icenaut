using UnityEngine;

public class MultiStateItem : Item
{
    private ItemState currentState;

    protected ItemState CurrentState => currentState;
    public new MultiStateItemData Data => (MultiStateItemData)itemData;

    protected virtual void ChangeState(ItemState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        newState.OnStateEnter();
        currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
