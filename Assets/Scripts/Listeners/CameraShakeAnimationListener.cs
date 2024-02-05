using UnityEngine;

public class CameraShakeAnimationListener : MonoBehaviour
{
    [SerializeField]
    private CameraShakeAnimationData OnPlayerHitCameraShakeAnimation;

    private CameraEffectsManager cameraEffectsManager;

    private void Start()
    {
        cameraEffectsManager = CameraEffectsManager.instance;
        EventManager.OnPlayerHit.AddListener(OnPlayerHit);
    }

    private void OnPlayerHit(Player player)
    {
        cameraEffectsManager.DoCameraShake(OnPlayerHitCameraShakeAnimation);
    }
}
