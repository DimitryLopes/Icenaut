using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBar : ActivatedInGame
{
    [SerializeField]
    private Image fill;
    [SerializeField]
    private float fadeCooldown;
    [SerializeField]
    private float fadeAnimationDuration;
    [SerializeField]
    private CanvasGroup canvasGroup;

    protected override void OnGameStarted()
    {
        gameObject.SetActive(true);
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        fill.fillAmount = Mathf.Clamp01(currentValue / maxValue);
    }

    public void SetUpBar(Color color)
    {
        fill.color = color;
    }

}
public enum UIPlayerBarType
{
    Health,
    Stamina,
}