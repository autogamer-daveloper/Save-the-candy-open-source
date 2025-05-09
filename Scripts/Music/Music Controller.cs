using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent (typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    /* Это музыкальный контроллер, который меняет треки после того, как трек либо закончился, либо
    сменилась сцена. Объект с этим скриптом не удалиться после смены сцены. Поэтому лучше не ссылаться
    на объект с этим скриптом, поскольку объекты из DontDestroyOnLoad не могут быть связаны с объектами
    в обычной сцене. Ещё этот контроллер связан с громкостью, которая находиться автоматически по тегу.*/

    [Header ("Settings - music")]
    [SerializeField] private AudioClip[] audioClips; /* гибкая настройка треков */
    [SerializeField] private float speed = 0.5f;

    private Slider _slider;
    private const string _sliderTag = "Volume Slider";

    private AudioSource audioSrc;
    private int _lastPlayedIndex = -1;
    private float volume;
    private Coroutine _playbackRoutine;

    private static MusicController _instance;

    private Tween _musicVolume;

#region While spawn music object

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        audioSrc = GetComponent<AudioSource>();
        _instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

#endregion

#region Registration to scene changing

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (_playbackRoutine != null)
        {
            StopCoroutine(_playbackRoutine);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Loaded_Scene();
    }

    private void Initialize_Slider()
    {
        var _strogare = new PlayerPrefsStorgare();
        volume = _strogare.MusicVolume;

        var _gameObject = GameObject.FindWithTag(_sliderTag);
        if (_gameObject != null) _slider = _gameObject.GetComponent<Slider>();
        if (_slider != null) {
        
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            _slider.value = volume;
        }
    }

#endregion

#region Music changing

    private void Loaded_Scene()
    {
        Initialize_Slider();
        Change_Music();
    }

    private void Change_Music()
    {
        CancelInvoke(nameof(Change_Music));

        _musicVolume = audioSrc.DOFade(0, speed).OnComplete(() => {
            int rand = Select_Clip();
            audioSrc.clip = audioClips[rand];
            audioSrc.Play();

            _musicVolume = audioSrc.DOFade(volume, speed).OnComplete(() => {
                Invoke(nameof(Change_Music), audioClips[rand].length);
            });
        });
    }

    private int Select_Clip()
    {
        if (audioClips.Length <= 1)
        return 0;

        int rand;
        do
        {
            rand = Random.Range(0, audioClips.Length);
        }
        while (rand == _lastPlayedIndex);

        _lastPlayedIndex = rand;
        return rand;
    }

#endregion

#region Slider

    private void OnSliderValueChanged(float value)
    {
        audioSrc.volume = value;

        var _strogare = new PlayerPrefsStorgare();
        _strogare.Save_Music_Volume(value);
    }

#endregion
}
