using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffectsManager : MonoBehaviour
{
    public static CameraEffectsManager instance;

    [SerializeField]
    private PostProcessVolume postProcessVolume;

    private bool isVignetteAnimating = false;
    private Vignette vignette;
    private VignetteAnimationData currentVignetteAnimationData;
    private CinemachineVirtualCamera mainCamera;
    private CinemachineBasicMultiChannelPerlin mainCameraChannels;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void AnimateVignette(VignetteAnimationData animationData)
    {
        if (vignette == null)
        {
            Vignette tempVignette;
            if (postProcessVolume.profile.TryGetSettings(out tempVignette))
            {
                vignette = tempVignette;
            }
        }
        PlayVignetteAnimation(animationData);
    }

    private void PlayVignetteAnimation(VignetteAnimationData animationData)
    {
        if (!isVignetteAnimating)
        {
            currentVignetteAnimationData = animationData;
            StartCoroutine(AnimateVignette());
        }
        else if(currentVignetteAnimationData.Priority < animationData.Priority)
        {
            FinishVignetteAnimation();
            StartVignetteAnimation(animationData);
        }
    }

    private void StartVignetteAnimation(VignetteAnimationData animationData)
    {
        currentVignetteAnimationData = animationData;
        StartCoroutine(AnimateVignette());
    }


    private IEnumerator AnimateVignette()
    {
        isVignetteAnimating = true;
        vignette.enabled.overrideState = true;
        float timer = 0;

        while (timer < currentVignetteAnimationData.Duration)
        {
            timer += Time.deltaTime;
            ApplyToVignette(timer/ currentVignetteAnimationData.Duration);
            Debug.Log(timer/ currentVignetteAnimationData.Duration);
            yield return null;
        }

        FinishVignetteAnimation();
    }

    private void ApplyToVignette(float curveValue)
    {
        vignette.intensity.value = currentVignetteAnimationData.IntensityCurve.Evaluate(curveValue);
        vignette.smoothness.value = currentVignetteAnimationData.SmoothnessCurve.Evaluate(curveValue);
    }

    private void FinishVignetteAnimation()
    {
        ApplyToVignette(1);
        if (currentVignetteAnimationData.HideOnFinish)
        {
            vignette.enabled.overrideState = false;
        }
        currentVignetteAnimationData = null;
        isVignetteAnimating = false;
    }

    public void DoCameraShake(CameraShakeAnimationData animationData)
    {
        if (mainCameraChannels == null)
        {
            mainCameraChannels = LevelManager.Instance.LevelCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        mainCameraChannels.m_AmplitudeGain = animationData.Amplitude;
        mainCameraChannels.m_FrequencyGain = animationData.Frequency;

        StartCoroutine(AnimateCameraShake(animationData.Duration));
    }

    private IEnumerator AnimateCameraShake(float duration)
    {
        float timer = 0;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        mainCameraChannels.m_AmplitudeGain = 0;
        mainCameraChannels.m_FrequencyGain = 0;
    }
}
