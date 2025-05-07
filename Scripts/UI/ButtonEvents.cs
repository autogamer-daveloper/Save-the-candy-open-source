using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonEvents : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    /* Этот скрипт добавляет плавную анимацию уменьшение и увеличение кнопки при наводке с помощью DOTween плагина. */

    [Header ("Setting - this button scaling")]
    [Range (0.1f, 2f)]
    [SerializeField] private float scaleMultiplier = 0.9f;
    [Range (0.1f, 2f)]
    [SerializeField] private float fadeSpeed = 0.5f;

    private RectTransform button;
    private Vector2 initialScale;
    private Vector2 pointedScale;

    private Tween tween;

    private void Start()
    {
        button = GetComponent<RectTransform>();
        
        initialScale = new Vector2(button.localScale.x, button.localScale.y);
        pointedScale = new Vector2(button.localScale.x * scaleMultiplier, button.localScale.y * scaleMultiplier);
    }

#region Events

    public void OnPointerEnter (PointerEventData eventData) 
    {
        /* Without fade animation */

        //button.localScale = initialScale * scaleMultiplier;

        /* With fade animation */

        if(tween != null) tween.Kill();
        tween = button.DOScale(pointedScale, fadeSpeed);
    }
    public void OnPointerExit  (PointerEventData eventData)
    {
        /* Without fade animation */

        //button.localScale = initialScale;

        /* With fade animation */

        if(tween != null) tween.Kill();
        tween = button.DOScale(initialScale, fadeSpeed);
    }

#endregion
}
