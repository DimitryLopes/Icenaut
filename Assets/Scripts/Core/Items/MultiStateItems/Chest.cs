using UnityEngine;

public class Chest : MultiStateItem
{
    [SerializeField]
    private int itemRewardAmount;
    [SerializeField]
    private ItemData itemRewardData;
    [SerializeField]
    private AnimationController animationController;
    [SerializeField]
    private AnimationInfo openAnimationInfo;
    [SerializeField]
    private ParticleSystem openChestParticles;

    private bool isInRange = false;

    private UnusedItemState closedState;
    private UsedItemState openState;

    private void Start()
    {
        SetUpStates();
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        CurrentState.Interact();
    }

    private void SetUpStates()
    {
        IsActive = true;
        foreach (ItemState state in Data.States)
        {
            if (state is UsedItemState used)
            {
                openState = new UsedItemState();
            }
            else if (state is UnusedItemState unused)
            {
                closedState = new UnusedItemState();
                closedState.OnInteractionEnabledTooltip = unused.OnInteractionEnabledTooltip;
            }
            else
            {
                Debug.LogError("There's a state in " + gameObject.name + " that is not being used");
            }
        }

        closedState.SetUp(this, OnClosedStateInteract, OnClosedStateInteractionEnabled, OnClosedStateInteractionDisabled);
        openState.SetUp(this);
        ChangeState(closedState);
    }

    private void GiveItemReward()
    {
        ItemManager.Instance.ChangeItemAmount(itemRewardData.Type, itemRewardAmount);
        openChestParticles.Play();
        animationController.Animate(openAnimationInfo);
    }

    #region States
    private void OnClosedStateInteract()
    {
        GiveItemReward();
        IsActive = false;
        ChangeState(openState);
    }

    private void OnClosedStateInteractionEnabled()
    {
        if (isInRange)
        {
            UIManager.Instance.ShowUITooltip(closedState.OnInteractionEnabledTooltip);
        }
    }

    private void OnClosedStateInteractionDisabled()
    {
        UIManager.Instance.HideUITooltip();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsActive)
            {
                isInRange = true;
                CurrentState.OnInteractionEnabled();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IsActive)
            {
                isInRange = false;
                CurrentState.OnInteractionDisabled();
            }
        }
    }
}
