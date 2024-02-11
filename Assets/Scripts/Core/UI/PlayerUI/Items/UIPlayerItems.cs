using System.Collections.Generic;
using UnityEngine;

public class UIPlayerItems : MonoBehaviour
{
    [SerializeField]
    private UIItemDisplay itemDisplayPrefab;

    private List<UIItemDisplay> Displays = new List<UIItemDisplay>();

    private void Start()
    {
        EventManager.OnLoadingGameFinished.AddListener(OnGameStarted);
    }

    private void OnGameStarted()
    {
        foreach(ItemData data in ItemManager.Instance.ItemDataBase)
        {
            UIItemDisplay display = GetAvailableDisplay();
            display.Create(data);
        }
    }

    private UIItemDisplay GetAvailableDisplay()
    {
       for(int i = 0; i < Displays.Count; i++)
        {
            if (!Displays[i].gameObject.activeSelf)
            {
                return Displays[i];
            }
        }

        UIItemDisplay display = Instantiate(itemDisplayPrefab, transform);
        Displays.Add(display);
        return display;
    }
}
