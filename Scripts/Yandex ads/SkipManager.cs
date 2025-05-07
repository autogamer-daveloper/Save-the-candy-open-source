using UnityEngine;

[RequireComponent(typeof(SkipEnemiesAd))]
[RequireComponent(typeof(MainHealth))]
public class SkipManager : MonoBehaviour
{
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

        if(_trapCount >= 4 && _trapDamage >= 4 && _enemyLevel >= 4)
        {
            adManager.Show_Skip_Button();
        }
    }
}
