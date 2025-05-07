using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AddClickSound : MonoBehaviour
{
    /* Этот скрипт добавляет на все кнопки на сцене звуковой эффект. */

    [Header ("Settings - click sound")]
    [SerializeField] private AudioClip click;

    private AudioSource audioSrc;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        audioSrc = GetComponent<AudioSource>();

        var _strogare = new PlayerPrefsStorgare();
        float value = _strogare.MusicVolume;

        audioSrc.volume = value;

        Button[] buttons = FindObjectsByType<Button>(0);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(Click_Sound);
        }
    }

    private void Click_Sound()
    {
        audioSrc.PlayOneShot(click);
    }
}
