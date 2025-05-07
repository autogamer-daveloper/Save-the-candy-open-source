using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    /* Скрипт для контроля главного меню */

    [Header ("Settings - menu")]
    [Space (5)]
    [Header ("UI")]
    [SerializeField] private GameObject exitUnloader;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button buttonLoadLevel;
    [SerializeField] private Button buttonSettingsShow;
    [SerializeField] private Button buttonSettingsHide;
    [SerializeField] private Button buttonDeleteProgress;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Color hardColor;
    [SerializeField] private Color normalColor;

    private string[] sceneList = {
        "level - 1",
        "level - 2",
        "level - 3",
        "level - 4",
        "level - 5"
    };

    private UniversalAnimationMovement _exitUnloader;
    private UniversalAnimationMovement _settingsPanel;

    private bool _isHardMode = false;

    private int sceneId;

    private void Start()
    {
        var _strogare = new PlayerPrefsStorgare();
        sceneId = _strogare.ProgressLevel;
        _isHardMode = _strogare.Hardcore;

        /* получается что уровен должен совпадать с scene index в настройках билда и в списке sceneList
        который отвечает за отображение проходимово сейчас уровня

        Массивы считаются не с 0, а с 1. в данном проекте до уровней может быть только 1 сцена, но мне этого достаточно.
        Если до уровней у вас кучу сцен, то можно привязать уровни к id сцен из настроек билда. К примеру:
        уровень 1 будет привязан к сцене 4. и так связать каждый уровень.
        */

        Initialize();
    }

    private void Initialize()
    {
        /* Но так как считаются массивы с 0, а не с 1, мне придёться использовать простую формулу x - 1.
        Иначе при уровне 5, мне бы показывало текстом уровень 6.
        */

        levelText.text = sceneList[sceneId - 1] + (_isHardMode ? " Hard mode!" : "");
        levelText.color = (_isHardMode ? hardColor : normalColor);

        _exitUnloader = exitUnloader.GetComponent<UniversalAnimationMovement>();
        _settingsPanel = settingsPanel.GetComponent<UniversalAnimationMovement>();
        buttonLoadLevel.onClick.AddListener(Load_Level);
        buttonSettingsShow.onClick.AddListener(Show_Settings);
        buttonSettingsHide.onClick.AddListener(Hide_Settings);
        buttonDeleteProgress.onClick.AddListener(Delete_Progress);
    }

#region Loading level

    private void Load_Level()
    {
        Enable_Unloader();

        Invoke("_Load_Level", 0.5f);
    }

    private void _Load_Level()
    {
        var loader = new SceneLoader();
        loader.LoadSceneByIndex(sceneId);
    }

    private void Enable_Unloader()
    {
        exitUnloader.SetActive(true);
        _exitUnloader.Show_And_Enable();
    }

#endregion

#region Settings

    private void Show_Settings()
    {
        settingsPanel.SetActive(true);
        _settingsPanel.Show_And_Enable();
    }

    private void Hide_Settings()
    {
        _settingsPanel.Hide_And_Disable();
    }

#endregion

#region Delete progress

    private void Delete_Progress()
    {
        var _strogare = new PlayerPrefsStorgare();
        _strogare.Delete_All();

        Reload_Menu();
    }

    private void Reload_Menu()
    {
        Enable_Unloader();
        Invoke("_Reload_Menu", 0.5f);
    }

    private void _Reload_Menu()
    {
        var loader = new SceneLoader();
        loader.LoadSceneByIndex(0);
    }

#endregion
}
