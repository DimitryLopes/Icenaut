using System;
using UnityEngine;
using DG.Tweening;

public class SimpleRotate : MonoBehaviour
{
    [Serializable]
    public class RotationConfig
    {
        public Vector3 rotationAmount;
        public float duration = 1f;
        public Ease easeType = Ease.Linear;
        public LoopType loopType = LoopType.Restart;
        public int loops = -1;
    }

    public RotationConfig rotationConfig;

    void Start()
    {
        Rotate();
    }

    public void Rotate()
    {
        transform.DORotate(rotationConfig.rotationAmount, rotationConfig.duration)
            .SetEase(rotationConfig.easeType)
            .SetLoops(rotationConfig.loops, rotationConfig.loopType);
    }
}