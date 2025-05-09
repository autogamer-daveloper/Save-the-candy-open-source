using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using System.Threading.Tasks;

public class TrapCount : MonoBehaviour
{
    /* Этот скрипт отвечает за количество ловушек на уровне. */

    [Header ("Settings - traps")]
    [SerializeField] private GameObject[] trap = new GameObject[5]; /* НЕ РЕДАКТИРОВАТЬ ДЛИНУ МАССИВА В ИНСПЕКТОРЕ! ПОЖАЛУЙСТА*/
    [SerializeField] private GameObject button;
    [SerializeField] private TMP_Text price;
    [SerializeField] private ButtonImageColor preset;
    [SerializeField] private int priceStart = 10; // Решил забабахать в каждом уровне свою цену
    [Header ("Settings - hardcore")]
    [SerializeField] private Hardcoremode hardScript;

    private Button _button;
    private Image _buttonImage;

    private Color _enable;
    private Color _disable;

    private PlayerPrefsStorgare _strogare;

    private int _level;
    private int _levelLocation;
    private int _price;

    private int _maxLevel = 5;
    
    private string _lang = "Max";

    private void Awake()
    {
        Button_Initialization();
    }

    private void Start()
    {
        Initialize();
        Check_Traps();
    }

#region Initialization

    private void Initialize()
    {
        _strogare = new PlayerPrefsStorgare();
        _level = _strogare.ProgressTrapCount;
        _levelLocation = _strogare.ProgressLevel;

        Count_Price();
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

        _button.onClick.AddListener(Add_New_Trap);

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

#region Adding new traps

    private void Add_New_Trap()
    {
        Debug.Log("Called Add_New_Trappa");

        int candies = _strogare.Candy;

        if(candies >= _price)
        {
            _strogare.Add_Candies(_price * -1);
            _strogare.Add_Trap();
        }

        Initialize();
        Check_Traps();
        Button_Initialization();
    }

    private void Check_Traps()
    {
        for(int i = 0; i < trap.Length; i++)
        {
            if(i < _level)
            {
                trap[i].SetActive(true);
            }
            else
            {
                trap[i].SetActive(false);
            }
        }
    }

#endregion

#region Change MAX word

    internal void Change_Max_Word(string word)
    {
        _lang = word;
        Count_Price();
    }

#endregion
}
