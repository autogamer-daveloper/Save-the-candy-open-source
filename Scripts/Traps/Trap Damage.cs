using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrapDamage : MonoBehaviour
{
    /* Этот скрипт отвечает за урон у ловушек, и если есть анимация, то и за скорость анимации. */

    [Header ("Settings - trap damage")]
    [SerializeField] private MobSpawner Spawner;
    [SerializeField] private TMP_Text price;
    [SerializeField] private GameObject button;
    [SerializeField] private ButtonImageColor preset;
    [SerializeField] private bool isAnimated = false;
    [SerializeField] private Animator[] trapAnimators = new Animator[5]; /* Ну пожалуйста, не менять ДЛИНУ массива. Спасибо! */
    [SerializeField] private int priceStart = 5; // Решил забабахать в каждом уровне свою цену
    [Header ("Settings - hardcore")]
    [SerializeField] private Hardcoremode hardScript;

    private float[] _animationSpeed = { 1f, 1.25f, 1.5f, 1.75f, 2f };

    private PlayerPrefsStorgare _strogare;
    private int _price;
    private int _level;
    private int _levelLocation;
    private int _maxLevel = 5;

    private Button _button;
    private Image _buttonImage;
    private Color _enable;
    private Color _disable;

    private void Awake()
    {
        Initialize_Button();
    }

    private void Start()
    {
        Initialize();
    }

#region Initialize

    private void Initialize()
    {
        _strogare = new PlayerPrefsStorgare();
        _level = _strogare.ProgressTrapDamageAndSpeed;
        _levelLocation = _strogare.ProgressLevel;

        Count_Price();
        Initialize_Speed();
    }

    private void Count_Price()
    {
        var eiler = new EilerFormula();

        float tmp_priceStart = eiler.EilerCounting((float)priceStart, _levelLocation, (hardScript.IsHardMode ? 2.7f : 1.5f));

        float tmp_price = eiler.EilerCounting(tmp_priceStart, _level, (hardScript.IsHardMode ? 2.7f : 1.5f));
        _price = Mathf.RoundToInt(tmp_price);

        if (_level < _maxLevel) price.text = _price.ToString();
        else price.text = "Max";
    }

    private void Initialize_Button()
    {
        _enable = preset.Enable;
        _disable = preset.Disable;

        _button = button.GetComponent<Button>();
        _buttonImage = button.GetComponent<Image>();

        _button.onClick.AddListener(Upgrade_Traps_And_Speed);

        if(_level >= _maxLevel)
        {
            _button.interactable = false;
            _buttonImage.color = _disable;
        }
        else
        {
            _button.interactable = true;
            _buttonImage.color = _enable;
        }
    }

    private void Initialize_Speed()
    {
        if (isAnimated)
        {
            foreach (Animator animator in trapAnimators)
            {
                animator.speed = _animationSpeed[_level - 1];
            }
        }
    }

#endregion

#region Button upgrader

    private void Upgrade_Traps_And_Speed()
    {
        Debug.Log("Upgrading traps called!");

        int candies = _strogare.Candy;
        
        if(candies >= _price)
        {
            _strogare.Add_Candies(_price * -1);
            _strogare.Upgrade_Traps();

            Initialize_Speed();
        }

        Initialize();
        Spawner.Initialize();
    }

#endregion
}
