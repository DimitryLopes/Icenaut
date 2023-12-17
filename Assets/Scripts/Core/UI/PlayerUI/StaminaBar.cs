using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : ActivatedInGame
{
    [SerializeField]
    private Image staminaFill;
    [SerializeField]
    private float fadeCooldown;
    [SerializeField]
    private float fadeAnimationDuration;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private Player player;

    protected override void OnGameStarted()
    {
        player = GameManager.Instance.CurrentPlayer;
    }

    void Update()
    {
        if (player != null)
        {
            staminaFill.fillAmount = player.CurrentStamina / player.MaxStamina;
        }
    }
}
