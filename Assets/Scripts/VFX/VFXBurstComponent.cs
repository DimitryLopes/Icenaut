using UnityEngine;

public class VFXBurstComponent : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    public void Burst()
    {
        particles.Play();
    }
}
