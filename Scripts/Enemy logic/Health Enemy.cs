using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    /* В этом скрипте просчитывается логика потери здоровья врага, а также прокачка урона по нему */

    [SerializeField] private GameObject healthBar;
    [SerializeField] private string TrapTag = "Trap";

    public int Reward = 1;

    private Slider _healthBar;
    private Animator _animator;
    private MoveObject _moveObject;
    private CapsuleCollider _collider;
    private GameObject _showCandies;
    private ShowCandies _showCandiesScript;
    private GameObject _mainGameLogic;
    private MainHealth _mainGameLogicScript;
    private Hardcoremode _hardScript;

    private float _healthTick;
    private bool _isTrapped = false;

    [HideInInspector]
    public float Health;

    [SerializeField]
    private float _healthMax;
    private float _damage;
    private float _damageScaler;

    private int _level;
    private int _levelEnemy;
    private int _levelDamage;

    private bool _playingDeath = false;

    private void Start()
    {
        Initialize();
        Count_Level_From_Strogare();
        Count_Tickrate();
    }

#region Counting main ints

    private void Initialize()
    {
        _healthBar = healthBar.GetComponent<Slider>();
        _animator = GetComponent<Animator>();
        _moveObject = GetComponent<MoveObject>();
        _collider = GetComponent<CapsuleCollider>();

        _showCandies = GameObject.FindWithTag("CandyShowObject");
        if(_showCandies != null) {
            _showCandiesScript = _showCandies.GetComponent<ShowCandies>();
        }

        _mainGameLogic = GameObject.FindWithTag("GameManager");
        if(_mainGameLogic != null) {
            _mainGameLogicScript = _mainGameLogic.GetComponent<MainHealth>();
            _hardScript = _mainGameLogic.GetComponent<Hardcoremode>();
        }
    }

    private void Count_Level_From_Strogare()
    {
        var strogare = new PlayerPrefsStorgare();
        _levelEnemy = strogare.ProgressMobSpawnAndMovement;
        _levelDamage = strogare.ProgressTrapDamageAndSpeed;
        _level = strogare.ProgressLevel;

        Count_Damage_Scaling();
        Count_Main_Ints();
    }

    private void Count_Damage_Scaling()
    {
        _damageScaler = Mathf.Pow(1.25f, _levelDamage);
    }

    private void Count_Main_Ints()
    {
        var eiler = new EilerFormula();

        float baseDamage = _healthMax * 0.75f;

        _healthMax = eiler.EilerCounting(_healthMax, _levelEnemy, (_hardScript.IsHardMode ? 2.7f : 1.5f));
        _damage = eiler.EilerCounting(baseDamage * _damageScaler, _levelDamage, (_hardScript.IsHardMode ? 2.7f : 1.5f));
        Health = _healthMax;

        _healthBar.maxValue = _healthMax;
        _healthBar.value = _healthMax;
    }

    private void Count_Tickrate()
    {
        var ticks = new Tickrate();
        _healthTick = ticks.Tick;
    }

#endregion

#region Get hit

    private void Check_Health()
    {
        if(_isTrapped)
        {
            Get_Hit(_damage);
        }
    }

    private void Get_Hit(float damage)
    {
        Health -= damage;
        if(Health > 0f) _healthBar.value = Health;
        else Play_Death();
    }

    private void Play_Death()
    {
        if(_playingDeath) return;

        Count_Reward();
        _showCandiesScript.Initialize();

        _mainGameLogicScript.Check_Killing();

        DisableMoving();
        _collider.enabled = false;

        _animator.SetTrigger("Death");

        Destroy(healthBar);
        Invoke("Death", 3f);

        _playingDeath = true;
    }

    private void Count_Reward()
    {
        var strogare = new PlayerPrefsStorgare();

        var eiler = new EilerFormula();
        float result = eiler.EilerCounting((float)Reward, _level, (_hardScript.IsHardMode ? 2.7f : 1.5f));
        int rewardResult = Mathf.RoundToInt(result);

        strogare.Killing_Someone(rewardResult);
    }

#endregion

#region Check hit

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TrapTag))
        {
            _isTrapped = true;
            InvokeRepeating(nameof(Check_Health), 0f, _healthTick);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(TrapTag))
        {
            _isTrapped = false;
            CancelInvoke(nameof(Check_Health));
        }
    }

#endregion

#region Other

    private void Death()
    {
        Destroy(gameObject);
    }

    private void DisableMoving()
    {
        _moveObject.enabled = false;
    }

#endregion
}
