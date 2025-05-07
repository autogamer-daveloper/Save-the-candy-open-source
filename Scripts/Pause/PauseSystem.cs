using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    /* Система паузы, которая не завязана на Time.timeScale = 0. По этой причине анимации не останавливаются, что делает игру чуточку живее.
    Пауза через Time.timeScale = 0 будет корректно работать только в том случае, если вы не используете анимации, а они зачастую завязаны на
    timeScale, который вы обычно выкручиваете в 0, что не есть хорошо. */

    [Header ("Settings - pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private MobSpawner spawner;

    [HideInInspector]
    public bool isPaused = false;

    private UniversalAnimationMovement _dotweenAnimations;

    private void Start()
    {
        _dotweenAnimations = pausePanel.GetComponent<UniversalAnimationMovement>();
        spawner.canSpawn = true;
    }

    public void Change_Time_Scale(bool isPlaying)
    {
        if(isPlaying)
        {
            _dotweenAnimations.Hide_And_Disable();
            spawner.canSpawn = true;
            isPaused = false;
        }
        else
        {
            pausePanel.SetActive(true);
            _dotweenAnimations.Show_And_Enable();
            spawner.canSpawn = false;
            isPaused = true;
        }
    }
}
