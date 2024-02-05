using UnityEngine;

public class VignetteAnimationListener : MonoBehaviour
{
    [SerializeField]
    private VignetteAnimationData OnPlayerHitVignetteAnimation;

    private CameraEffectsManager cameraEffectsManager;

    private void Start()
    {
        cameraEffectsManager = CameraEffectsManager.instance;
        EventManager.OnPlayerHit.AddListener(OnPlayerHit);
    }

    private void OnPlayerHit(Player player)
    {
        cameraEffectsManager.AnimateVignette(OnPlayerHitVignetteAnimation);
    }
}
