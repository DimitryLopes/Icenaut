using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(fileName = "VignetteAnimationData", menuName = "Animation/Vignette Animation Data")]
public class VignetteAnimationData : CameraAnimation
{
    [SerializeField]
    private AnimationCurve intensityCurve;
    [SerializeField]
    private AnimationCurve smoothnessCurve;

    public AnimationCurve IntensityCurve => intensityCurve;
    public AnimationCurve SmoothnessCurve => smoothnessCurve;
}
