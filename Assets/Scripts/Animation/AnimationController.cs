using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void Animate(string animationKey, AnimationType type, bool value = true)
    {
        if (type == AnimationType.Bool)
        {
            animator.SetBool(animationKey, value);
        }
        else if(type == AnimationType.Trigger)
        {
            animator.SetTrigger(animationKey);
        }
    }

    public void Animate(AnimationInfo animationInfo, bool value = true)
    {
        if (animationInfo.animationType == AnimationType.Bool)
        {
            animator.SetBool(animationInfo.animationKey, value);
        }
        else if (animationInfo.animationType == AnimationType.Trigger)
        {
            animator.SetTrigger(animationInfo.animationKey);
        }
    }

    public enum AnimationType
    {
        Bool,
        Trigger,
    }
}
