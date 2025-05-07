using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkipEnemiesAd : MonoBehaviour
{
    [Header ("Settings - ads")]
    [SerializeField] private YandexGame sdk;
    [SerializeField] private Button button;

    private MainHealth gameManager;

    private void Start()
    {
        button.interactable = false;
        button.onClick.AddListener(Show_Ad);

        gameManager = GetComponent<MainHealth>();
    }

#region Ability to skip level

    /* при максимальной прокачке будет доступен пропуск уровня */

    internal void Show_Skip_Button()
    {
        button.interactable = true;
    }

#endregion

#region Ads manager

    private void Show_Ad()
    {
        sdk._RewardedShow(1);
    }

    public void Skip()
    {
        gameManager.Next_Level_Win();
    }

#endregion
}
