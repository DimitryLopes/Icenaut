using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Scriptable Objects/Power Up")]
public class PowerUpData : ScriptableObject
{
    [SerializeField]
    private AudioClip sfx;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Texture2D clothTexture;
    [SerializeField]
    private int id;

    public float Speed => speed;
    public AudioClip SFX => sfx;
    public int ID => id;
    public float JumpForce => jumpForce;
    public float Duration => duration;
    public Texture2D Texture => clothTexture;

    public void CreateCustom(PowerUpData data, float duration)
    {
        speed = data.Speed;
        jumpForce = data.JumpForce;
        this.duration = duration;
        clothTexture = data.Texture;
        id = data.ID;
    }
}