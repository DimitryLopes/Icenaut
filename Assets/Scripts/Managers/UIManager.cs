using UnityEngine;
using System.Collections.Generic;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private UITooltip tooltip;
    [SerializeField]
    private LoadingScreen loadingScreen;
    [SerializeField]
    private Transform UIPlayerInfoContainer;

    [SerializeField, Space]
    private List<UIPlayerBarInfo> playerUIInfo;
    [SerializeField]
    private UIPlayerBar playerBarPrefab;
    [SerializeField]
    private Transform UIPlayerBarsContainer;

    public static UIManager Instance;

    private Dictionary<UIPlayerBarType, UIPlayerBar> UIBarPool = new Dictionary<UIPlayerBarType, UIPlayerBar>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            EventManager.OnStateActivated.AddListener(OnStateChanged);
            return;
        }
        Destroy(gameObject);
    }

    public void OnStateChanged(GameState state)
    {
        if(state.EnumID == GameStates.LoadingGame)
        {
            ShowPlayerUI();
            foreach(UIPlayerBarInfo info in playerUIInfo)
            {
                if (!UIBarPool.ContainsKey(info.UIPlayerBarType))
                {
                    var bar = Instantiate(playerBarPrefab, UIPlayerBarsContainer);
                    bar.SetUpBar(info.PlayerBarColor);
                    UIBarPool.Add(info.UIPlayerBarType, bar);
                }
            }
        }
        else if(state.EnumID == GameStates.Finished)
        {
            HidePlayerUI();
        }
    }

    public void ShowPlayerUI()
    { 
        UIPlayerInfoContainer.gameObject.SetActive(true);
    }

    public void HidePlayerUI()
    {
        UIPlayerInfoContainer.gameObject.SetActive(false);
    }

    public void ShowLoadingScreen(AsyncOperation operation)
    {
        loadingScreen.Show(operation);
    }

    public void ShowUITooltip(string text)
    {
        tooltip.Show(text);
    }

    public void HideUITooltip()
    {
        tooltip.Hide();
    }

    public void UpdatePlayerBar(UIPlayerBarType type, float currentValue, float maxValue)
    {
        UIBarPool[type].UpdateBar(currentValue, maxValue);
    }

    [Serializable]
    private struct UIPlayerBarInfo
    {
        [SerializeField]
        private Color playerBarColor;
        [SerializeField]
        private UIPlayerBarType uiPlayerBarType;

        public UIPlayerBarType UIPlayerBarType => uiPlayerBarType; 
        public Color PlayerBarColor => playerBarColor;
    }

    public enum UIPlayerBarType
    {
        Health,
        Stamina,
    }
}
