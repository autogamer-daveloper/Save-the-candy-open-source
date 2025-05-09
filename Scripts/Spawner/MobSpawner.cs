using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using System.Threading.Tasks;

public class MobSpawner : MonoBehaviour
{
    /* Это спавнер мобов, тут они прокачиваются и всё в таком духе. Когда ты прокачиваешь врагов, ты не сделаешь моментально сильнее,
    ты просто начнёшь спавнить мобов с более сильными параметрами. */

    [Header ("Settings - spawn")]
    [SerializeField] private GameObject mob;
    [SerializeField] private GameObject eliteMob;
    [Space (5)]
    [SerializeField] private float timer = 1f;
    [Range (1, 10)]
    [SerializeField] private int RewardUpscaler = 1;
    [SerializeField] private int priceStart = 7; // Решил забабахать в каждом уровне свою цену
    [Header ("Settings - UI")]
    [SerializeField] private GameObject button;
    [SerializeField] private ButtonImageColor preset;
    [SerializeField] private TMP_Text price;
    [Header ("Settings - hardcore")]
    [SerializeField] private Hardcoremode hardScript;

    [HideInInspector]
    public bool canSpawn = true;

    private PlayerPrefsStorgare _strogare;

    [Range (1, 10)]
    private int _level;
    private int _levelLocation;
    private float randomChanse;
    private int _price;
    private int _maxLevel = 5;
    private float _increase = 0.05f;
    private float _baseSpeed = 5f;

    private GameObject _invokedMob;

    private Button _button;
    private Image _buttonImage;
    private Color _enable;
    private Color _disable;

    private void Awake()
    {
        Button_Initialization();
    }

    private void Start()
    {
        Initialize();
    }

#region Initialization

    public void Initialize()
    {
        _strogare = new PlayerPrefsStorgare();
        _level = _strogare.ProgressMobSpawnAndMovement;
        _levelLocation = _strogare.ProgressLevel;

        Count_Price();
        Count_Timer();
    }

    private void Count_Timer()
    {
        float result = Mathf.Max(0.1f, 1f - (((float)_level - 1) * _increase));
        timer = Mathf.Round(result);

        Invoke("Spawn", timer);
    }

    private void Count_Price()
    {
        var eiler = new EilerFormula();

        float tmp_priceStart = eiler.EilerCounting((float)priceStart, _levelLocation, (hardScript.IsHardMode ? 2.7f : 1.5f));

        float tmp_price = eiler.EilerCounting(tmp_priceStart, _level, (hardScript.IsHardMode ? 2.7f : 1.5f));
        _price = Mathf.RoundToInt(tmp_price);

        if (_level < _maxLevel) {
            price.text = _price.ToString();
        }
        else {
            price.text = "";
        }
    }

    private void Button_Initialization()
    {
        _enable = preset.Enable;
        _disable = preset.Disable;

        _button = button.GetComponent<Button>();
        _buttonImage = button.GetComponent<Image>();

        _button.onClick.AddListener(Upgrade_Enemies);

        if (_level < _maxLevel) {
            _button.interactable = true;
            _buttonImage.color = _enable;
        }
        else {
            _button.interactable = false;
            _buttonImage.color = _disable;
        }
    }

#endregion

#region Upgrade

    private void Upgrade_Enemies()
    {
        int candies = _strogare.Candy;

        if(candies >= _price)
        {
            _strogare.Add_Candies(_price * -1);
            _strogare.Upgrade_Mob_Spawner_And_Movement();
        }

        Initialize();
        Button_Initialization();
    }

#endregion

#region Spawner

    private void Spawn()
    {
        CancelInvoke("Spawn");

        Invoke("Spawn", timer);

        if(canSpawn == false) return;

        float result = _level * 5;
        randomChanse = Mathf.Round(result);

        int i = Random.Range(0, 100);

        if(i <= randomChanse)
        {
            Spawn_Mob(eliteMob);
        }
        else
        {
            Spawn_Mob(mob);
        }
    }

    private void Spawn_Mob(GameObject obj)
    {
        float random = Random.Range(-2.5f, 2.5f);
        var randPos = new Vector3(gameObject.transform.position.x + random,
        gameObject.transform.position.y,
        gameObject.transform.position.z);

        _invokedMob = Instantiate(obj, randPos, gameObject.transform.rotation);

        var eiler = new EilerFormula();

        var _invokedMobMovement = _invokedMob.GetComponent<MoveObject>();
        var _invokedMobHealth = _invokedMob.GetComponent<HealthEnemy>();

        _invokedMobHealth.Reward += _level * RewardUpscaler;
        _invokedMobHealth.Health = eiler.EilerCounting(_invokedMobHealth.Health, _level, 2.7f);
        _invokedMobMovement.Speed = _baseSpeed + (((float)_level - 1) * _increase);
    }

#endregion
}
