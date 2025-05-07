using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    [Header ("Settings - hyperlink")]
    [SerializeField] private Button openLink;
    [SerializeField] private string hyperLink;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        openLink.onClick.AddListener(OpenHyperlink);
    }

    private void OpenHyperlink()
    {
        Application.OpenURL(hyperLink);
    }
}
