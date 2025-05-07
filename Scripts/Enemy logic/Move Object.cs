using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float Speed;

    [SerializeField] private float finalPositionZ = -50;

    private int _level;
    private Transform _thisEnemy;
    private Vector3 _endPosition;

    private PauseSystem _pause;

    private void Start()
    {
        Initialize();

        Count_Speed_Level();
    }

    private void Initialize()
    {
        _thisEnemy = GetComponent<Transform>();
        var pause = GameObject.FindWithTag("PauseObject");
        if(pause != null) _pause = pause.GetComponent<PauseSystem>();
    }

    private void Count_Speed_Level()
    {
        var _strogare = new PlayerPrefsStorgare();
        _level = _strogare.ProgressMobSpawnAndMovement;

        Speed = ((float)_level / 2) * 10;
    }

    private void FixedUpdate()
    {
        if(_pause.isPaused == true) return;

        var _finalPosition = new Vector3(transform.position.x,
            transform.position.y,
            finalPositionZ);

        _thisEnemy.position = Vector3.MoveTowards(_thisEnemy.position, _finalPosition, Speed * Time.deltaTime); 
    }
}
