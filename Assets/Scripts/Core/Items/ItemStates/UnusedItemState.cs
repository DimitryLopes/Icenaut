
using UnityEngine;
[CreateAssetMenu(fileName = "UnusedItemState", menuName = "Scriptable Objects/Item States/Unused Item State")]
public class UnusedItemState : ItemState
{
    [HideInInspector]
    public string OnInteractionEnabledTooltip;


    public override void OnInteractionDisabled()
    {
        UIManager.Instance.HideUITooltip();
    }

    public override void OnInteractionEnabled()
    {
        UIManager.Instance.ShowUITooltip(OnInteractionEnabledTooltip);
    }
}
