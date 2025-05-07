using UnityEngine;
using UnityEngine.UI;

public class Hardcoremode : MonoBehaviour
{
    /* Почему я не сделал этот скрипт без MonoBehaviour, и чтобы он работал как PlayerPrefsStrogare? А всё из-за UI.
    Ведь мне нужно будет указать где то UI, а я не хочу каждый раз искать объекты UI.
    поэтому этот скрипт я повешу на какой либо объект в игре, а все скрипты, что должны
    ссылаться на него, будут инитиализированы через Inspector в Unity, это гораздо удобнее.
    Короче. Этот скрипт отвечает за уровень сложности. */

    [Header ("Settings - hardcore")]
    [SerializeField] private Image modeImage;
    [SerializeField] private Sprite normalMode;
    [SerializeField] private Sprite hardMode;

    [HideInInspector]
    public bool IsHardMode = false;

    private void Start()
    {
        var _strogare = new PlayerPrefsStorgare();
        IsHardMode = _strogare.Hardcore;

        Initialize();
    }

    private void Initialize()
    {
        if(IsHardMode) Hard_Mode();
        else Normal_Mode();
    }

    private void Hard_Mode()
    {
        modeImage.sprite = hardMode;
    }

    private void Normal_Mode()
    {
        modeImage.sprite = normalMode;
    }
}
