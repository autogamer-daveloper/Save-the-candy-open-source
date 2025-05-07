using UnityEngine;

public class PauseSystem : MonoBehaviour
{
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
