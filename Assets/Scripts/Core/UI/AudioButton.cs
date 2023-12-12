using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayAudio);
    }

    private void PlayAudio()
    {

    }
}
