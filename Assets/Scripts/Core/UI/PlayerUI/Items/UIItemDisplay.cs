using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Image itemIconImage;
    [SerializeField]
    private TextMeshProUGUI itemAmountText;

    private ItemType type;

    public string ItemAmountText => itemAmountText.text;

    public void Create(ItemData data)
    {
        type = data.Type;
        itemIconImage.sprite = data.Icon;
        itemAmountText.text = ItemManager.Instance.GetItemAmount(type).ToString();
        EventManager.OnItemAmountChanged.AddListener(OnItemAmountChanged);
    }

    public void OnItemAmountChanged(ItemType type, int amount)
    {
        if(type == this.type)
        {
            itemAmountText.text = ItemManager.Instance.GetItemAmount(type).ToString();
        }
    }
}
