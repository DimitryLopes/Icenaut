using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeAnimationData", menuName = "Animation/Camera Shake Animation Data")]
public class CameraShakeAnimationData : CameraAnimation
{
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float frequency;

    public float Amplitude => amplitude;
    public float Frequency => frequency;
}
