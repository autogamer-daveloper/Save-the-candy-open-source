using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCandies : MonoBehaviour
{
    /* Этот скрипт показывает сколько у вас конфеток на данный момент. (По вызову Initialize) */

    private TMP_Text _text;
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _text = GetComponent<TMP_Text>();
        var strogare = new PlayerPrefsStorgare();
        int candies = strogare.Candy;
        _text.text = candies.ToString();
    }
}
