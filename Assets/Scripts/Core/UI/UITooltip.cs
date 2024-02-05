using TMPro;
using UnityEngine;

public class UITooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject textContainer;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Start()
    {
        textContainer.SetActive(false);
    }

    public void Show(string text)
    {
        this.text.text = text;
        textContainer.SetActive(true);
    }

    public void Hide()
    {
        textContainer.SetActive(false);
    }
}
