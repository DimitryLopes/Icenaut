using System.Collections;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private const string SHADER_ID_NAME = "_EmissionMap";
    private Texture defaultTexture;

    public static ClothingManager Instance;
    public float RemainingClothingTimer { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void ChangePlayerClothing(Texture texture, float duration)
    {
        StartCoroutine(ChangeClothes(texture, duration));
    }

    private IEnumerator ChangeClothes(Texture texture, float duration)
    {
        float timer = 0;

        if(meshRenderer == null)
        {
            GetPlayerMesh();
        }

        meshRenderer.materials[0].SetTexture(SHADER_ID_NAME, texture);
        while(timer < duration)
        {
            timer += Time.deltaTime;
            RemainingClothingTimer = duration - timer;
            yield return null;
        }
        RemainingClothingTimer = 0;
        meshRenderer.materials[0].SetTexture(SHADER_ID_NAME, defaultTexture);
    }

    private void GetPlayerMesh()
    {
        meshRenderer = GameManager.Instance.CurrentPlayer.MeshRenderer;
        defaultTexture = meshRenderer.materials[0].GetTexture(SHADER_ID_NAME);
    }
}
