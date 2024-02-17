using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Scriptable Objects/Power Up")]
public class PowerUpData : ScriptableObject
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Texture2D clothTexture;

    public float Speed => speed;
    public float JumpForce => jumpForce;
    public float Duration => duration;
    public Texture2D Texture => clothTexture;
}