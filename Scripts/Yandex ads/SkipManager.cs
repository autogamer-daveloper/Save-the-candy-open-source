using UnityEngine;

[RequireComponent(typeof(SkipEnemiesAd))]
[RequireComponent(typeof(MainHealth))]
public class SkipManager : MonoBehaviour
{
    /* Дополнение к скрипту SkipEnemiesAd */

    private SkipEnemiesAd adManager;

    private void Start()
    {
        adManager = GetComponent<SkipEnemiesAd>();
    }

    internal void Update_Values()
    {
        var strogare = new PlayerPrefsStorgare();
        int _trapCount = strogare.ProgressTrapCount;
        int _trapDamage = strogare.ProgressTrapDamageAndSpeed;
        int _enemyLevel = strogare.ProgressMobSpawnAndMovement;

        if(_trapCount >= 5 && _trapDamage >= 5 && _enemyLevel >= 5)
        {
            adManager.Show_Skip_Button();
        }
    }
}
