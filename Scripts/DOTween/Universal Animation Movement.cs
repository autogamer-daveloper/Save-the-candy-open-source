using UnityEngine;
using DG.Tweening;

public class UniversalAnimationMovement : MonoBehaviour
{
    /* Этот универсальный код используется для старта и конца анимации RectTransform на основе DOTween */

    [Header("Settings - transform")]
    [SerializeField] private Vector2 finalPosition;
    [Range(0.1f, 5f)]
    [SerializeField] internal float timer = 0.5f;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    private Tween tween;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        DOTween.KillAll();

        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void Show_And_Enable()
    {
        if (rectTransform == null) return;
        gameObject.SetActive(true);

        tween = rectTransform.DOAnchorPos(finalPosition, timer).OnComplete(() =>
        {
            tween.Kill();
        });
    }

    public void Hide_And_Disable()
    {
        if (rectTransform == null) return;

        tween = rectTransform.DOAnchorPos(startPosition, timer).OnComplete(() =>
        {
            tween.Kill();
            gameObject.SetActive(false);
        });
    }
}
