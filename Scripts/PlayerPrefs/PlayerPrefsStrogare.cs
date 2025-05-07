using UnityEngine;

public class PlayerPrefsStorgare
{
    /* Ты попал в скрипт, где инитиализируются игровые ключи игрока, что были сохранены во время геймплея.
    Скрипт автоматически всё инитиализирует и работает с PlayerPrefs. Легко подключать к любому скрипту.
    Соблюдён принцип инкапсуляции и полиморфизма.*/

    private const string _candyKey = "p_Candies";
    private const string _healthKey = "p_Health";
    private const string _hardCore = "p_HardMode";
    private const string _progressLevelKey = "p_ProgressLevel";
    private const string _progressLevelKilledKey = "p_ProgressLevelKilled";
    private const string _progressMobSpawnAndMovementKey = "p_ProgressMobSpawnAndMovement";
    private const string _progressTrapCountKey = "p_ProgressTrapCount";
    private const string _progressTrapDamageAndSpeedKey = "p_ProgressTrapDamageAndSpeed";
    private const int _maxLevel = 5;
    private const string _filledHp = "FilledHP";
    private const string _musicVolume = "Sound_Volume";
    private const string _isPlayerEntered = "Entered";

    public bool Hardcore
    {
        get => PlayerPrefs.HasKey(_hardCore) && PlayerPrefs.GetInt(_hardCore, 0) == 1;
        set => PlayerPrefs.SetInt(_hardCore, value ? 1 : 0);
    }

    public int Entered
    {
        get => PlayerPrefs.GetInt(_isPlayerEntered, 0);
        set => PlayerPrefs.SetInt(_isPlayerEntered, value);
    }

    public int FilledHealth
    {
        get => PlayerPrefs.GetInt(_filledHp, 0);
        set => PlayerPrefs.SetInt(_filledHp, value);
    }

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat(_musicVolume, 1);
        set => PlayerPrefs.SetFloat(_musicVolume, value);
    }

    public int Candy
    {
        get => PlayerPrefs.GetInt(_candyKey, 0);
        set => PlayerPrefs.SetInt(_candyKey, value);
    }

    public int Health
    {
        get => PlayerPrefs.GetInt(_healthKey, 0);
        set => PlayerPrefs.SetInt(_healthKey, value);
    }

    public int ProgressLevel
    {
        get => PlayerPrefs.GetInt(_progressLevelKey, 1);
        set => PlayerPrefs.SetInt(_progressLevelKey, value);
    }

    public int ProgressLevelKilled
    {
        get => PlayerPrefs.GetInt(_progressLevelKilledKey, 1);
        set => PlayerPrefs.SetInt(_progressLevelKilledKey, value);
    }

    public int ProgressMobSpawnAndMovement
    {
        get => PlayerPrefs.GetInt(_progressMobSpawnAndMovementKey, 1);
        set => PlayerPrefs.SetInt(_progressMobSpawnAndMovementKey, value);
    }

    public int ProgressTrapCount
    {
        get => PlayerPrefs.GetInt(_progressTrapCountKey, 1);
        set => PlayerPrefs.SetInt(_progressTrapCountKey, value);
    }

    public int ProgressTrapDamageAndSpeed
    {
        get => PlayerPrefs.GetInt(_progressTrapDamageAndSpeedKey, 1);
        set => PlayerPrefs.SetInt(_progressTrapDamageAndSpeedKey, value);
    }

    public void Save_Progress()
    {
        PlayerPrefs.Save();
    }

    public void Delete_All()
    {
        Reset_Progress();
        PlayerPrefs.DeleteKey(_progressLevelKey);
        PlayerPrefs.DeleteKey(_hardCore);
    }

    public void Reset_Progress()
    {
        Delete_Info_About_Health();
        PlayerPrefs.DeleteKey(_progressMobSpawnAndMovementKey);
        PlayerPrefs.DeleteKey(_progressTrapCountKey);
        PlayerPrefs.DeleteKey(_progressTrapDamageAndSpeedKey);
        PlayerPrefs.DeleteKey(_candyKey);
        PlayerPrefs.DeleteKey(_progressLevelKilledKey);
    }

    public void Next_Level()
    {
        Delete_Info_About_Health();
        PlayerPrefs.DeleteKey(_progressMobSpawnAndMovementKey);
        PlayerPrefs.DeleteKey(_progressTrapCountKey);
        PlayerPrefs.DeleteKey(_progressTrapDamageAndSpeedKey);
        PlayerPrefs.DeleteKey(_candyKey);
        PlayerPrefs.DeleteKey(_progressLevelKilledKey);
        ProgressLevel++;

        if(ProgressLevel > _maxLevel)
        {
            ProgressLevel = 1;
            PlayerPrefs.SetInt(_hardCore, 1);
        }

        Save_Progress();
    }

#region Upgrade something

    public void Upgrade_Mob_Spawner_And_Movement()
    {
        ProgressMobSpawnAndMovement++;
        Save_Progress();
    }

    public void Add_Trap()
    {
        ProgressTrapCount++;
        Save_Progress();
    }

    public void Upgrade_Traps()
    {
        ProgressTrapDamageAndSpeed++;
        Save_Progress();
    }

#endregion

#region Actions

    public void Add_Candies(int count)
    {
        Candy += count;
        Save_Progress();
    }

    public void Killing_Someone(int reward)
    {
        ProgressLevelKilled++;

        Candy = Candy + reward;

        Save_Progress();
    }

    public void Change_Health(int damage)
    {
        Health = damage;
        Save_Info_About_Health();
        Save_Progress();
    }

    public void This_Level(int id)
    {
        ProgressLevel = id;
        Save_Progress();
    }

    public void Delete_Info_About_Health()
    {
        FilledHealth = 0;
        Save_Progress();
    }

    public void Save_Info_About_Health()
    {
        FilledHealth = 1;
        Save_Progress();
    }

    public void Save_Music_Volume(float volume)
    {
        MusicVolume = volume;
    }

    public void EnterTheGame()
    {
        Entered++;
        Save_Progress();
    }

#endregion
}
