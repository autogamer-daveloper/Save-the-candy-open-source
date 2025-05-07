using UnityEngine;
using UnityEngine.UI;

public class StartPlaying : MonoBehaviour
{
    [Header ("Settings - first start")]
    [SerializeField] private GameObject panelLanguage;
    [SerializeField] private GameObject panelWarning;
    [SerializeField] private Button[] anyButton;
    [SerializeField] private GameObject panelUnloader;

    private float _speed;
    private UniversalAnimationMovement _animation;

    private const int _sceneId = 0;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var _strogare = new PlayerPrefsStorgare();
        int entered = _strogare.Entered;

        _animation = panelUnloader.GetComponent<UniversalAnimationMovement>();
        _speed = _animation.timer;

        switch(entered)
        {
            case 0:
                panelLanguage.SetActive(true);
            break;
            case 1:
                panelWarning.SetActive(true);
            break;
        }

        foreach(Button button in anyButton)
        {
            button.onClick.AddListener(Add_Entering);
        }
    }

    private void Add_Entering()
    {
        var _strogare = new PlayerPrefsStorgare();
        _strogare.EnterTheGame();

        _animation.Show_And_Enable();

        Invoke("Reload", _speed);
    }

    private void Reload()
    {
        var loader = new SceneLoader();
        loader.LoadSceneByIndex(_sceneId);
    }
}
