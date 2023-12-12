using System;
using UnityEngine;
using static AnimationController;

public class AnimationManager : MonoBehaviour
{
    [HideInInspector]
    public AnimationController PlayerAnimationController;

    public static AnimationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

}

[Serializable]
public struct AnimationInfo
{
    public string animationKey;
    public AnimationType animationType;
}
