using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainHealth : MonoBehaviour
{
    /* Этот скрипт проверяет твоё игровое здоровье, кол-во убитых персонажей, а также обновляет UI и просчитывает
    победил ли ты, или проиграл.*/

    [Header ("Settings - your health")]
    [SerializeField] private int yourHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider killingBar;
    [SerializeField] private string enemyTag = "Enemy";
    [Header ("Settings - UI")]
    [SerializeField] private TMP_Text textHealth;
    [SerializeField] private TMP_Text textKilled;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject enterLoader;
    [SerializeField] private GameObject exitUnloader;
    [SerializeField] private Button buttonRevive;
    [SerializeField] private Button buttonNextLevel;
    [SerializeField] private Button buttonMainMenu;
    [Header ("Settings - hardcore")]
    [SerializeField] private Hardcoremode hardScript;
    [Header ("Settings - skip ability")]
    [SerializeField] private SkipManager skipManager;

    private PlayerPrefsStorgare _strogare;

    private UniversalAnimationMovement _losePanel;
    private UniversalAnimationMovement _wonPanel;
    private UniversalAnimationMovement _mainPanel;
    private UniversalAnimationMovement _enterLoader;
    private UniversalAnimationMovement _exitUnloader;

    private int _level;

    private bool _canBeDamaged = true;

    private const int _healthBaseFixed = 1500;
    private int _healthMax;
    private int _health;
    private int _damage;

    private const int _killingBaseFixed = 100;
    private int _killingMax;
    private int _killing;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(enemyTag))
        {
            Receive_Damage(_damage);
            Destroy(other);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _strogare = new PlayerPrefsStorgare();
        _level = _strogare.ProgressLevel;

        Initialize_Animations();
        Count_Health_And_Damage();
        Initialize_Buttons();
    }

    private void Initialize_Animations()
    {
        _losePanel = losePanel.GetComponent<UniversalAnimationMovement>();
        _wonPanel = wonPanel.GetComponent<UniversalAnimationMovement>();
        _mainPanel = mainPanel.GetComponent<UniversalAnimationMovement>();
        _enterLoader = enterLoader.GetComponent<UniversalAnimationMovement>();
        _exitUnloader = exitUnloader.GetComponent<UniversalAnimationMovement>();

        _mainPanel.Show_And_Enable();
        _enterLoader.Show_And_Enable();
    }

    private void Count_Health_And_Damage()
    {
        var eiler = new EilerFormula();
        float tmp_healthMax = eiler.EilerCounting((float)_healthBaseFixed, _level, (hardScript.IsHardMode ? 2.7f : 1.5f));
        float tmp_killingMax = eiler.EilerCounting((float)_killingBaseFixed, _level, (hardScript.IsHardMode ? 1.5f : 1.25f));
        float tmp_damage = eiler.EilerCounting((float)_healthBaseFixed * 0.07f, _level, (hardScript.IsHardMode ? 3.5f : 2.5f));
        _healthMax = Mathf.RoundToInt(tmp_healthMax);
        _killingMax = Mathf.RoundToInt(tmp_killingMax);
        _damage = Mathf.RoundToInt(tmp_damage);

        if(_strogare.FilledHealth == 1)
        {
            Debug.Log("Get health");
            int i = _strogare.Health;
            Debug.Log($"Your loaded health - {i}");
            Fill_Health(i);
        }
        else
        {
            Debug.Log("Fill health");
            Fill_Health(_healthMax);
        }

        healthBar.maxValue = _healthMax;

        UpdateUI();
    }

    private void Initialize_Buttons()
    {
        buttonRevive.onClick.AddListener(Revive);
        buttonNextLevel.onClick.AddListener(Next_Level_Win);
        buttonMainMenu.onClick.AddListener(Load_Menu);
    }

    private void Receive_Damage(int damage)
    {
        if(!_canBeDamaged) return;

        _health -= damage;
        healthBar.value = _health;
        Check_Health();

        _strogare.Change_Health(_health);
    }

    private void Fill_Health(int fullHealth)
    {
        _health = fullHealth;
        healthBar.value = _health;
        Check_Health();

        _strogare.Change_Health(_health);
    }

    public void Check_Killing()
    {
        _killing = _strogare.ProgressLevelKilled;
        _killing = Math.Clamp(_killing, 0, _killingMax);

        if(_killing >= _killingMax)
        {
            wonPanel.SetActive(true);
            _wonPanel.Show_And_Enable();
        }

        UpdateUI();
    }

    private void Check_Health()
    {
        if(_health <= 0) Death();

        _health = Math.Clamp(_health, 0, _healthMax);
    }

    private void UpdateUI()
    {
        _killing = _strogare.ProgressLevelKilled;
        _killing = Math.Clamp(_killing, 0, _killingMax);

        textHealth.text = $"{_healthMax} / {_health}";
        textKilled.text = $"{_killingMax} / {_killing}";

        healthBar.value = _health;
        killingBar.maxValue = _killingMax;
        killingBar.value = _killing;

        skipManager.Update_Values();
    }

#region endings

    private void Death()
    {
        losePanel.SetActive(true);
        _strogare.Delete_Info_About_Health();
        _losePanel.Show_And_Enable();
    }

    private void Revive()
    {
        _strogare.Delete_Info_About_Health();
        _strogare.Reset_Progress();

        Load_Menu();
    }

    public void Next_Level_Win()
    {
        _strogare.Delete_Info_About_Health();
        _strogare.Next_Level();

        Load_Menu();
    }

#endregion

    private void Load_Menu()
    {
        exitUnloader.SetActive(true);
        _exitUnloader.Show_And_Enable();

        Invoke("_Load_Menu", 0.5f);
    }

    private void _Load_Menu()
    {
        int sceneId = 0;
        var sceneLoader = new SceneLoader();

        sceneLoader.LoadSceneByIndex(sceneId);
    }
}
