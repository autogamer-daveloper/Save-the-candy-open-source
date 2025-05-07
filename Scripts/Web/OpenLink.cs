using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    /* С помощью этого скрипта вы можете выбрать определённую кнопку, которая будет вести на определённые сайты. */

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
