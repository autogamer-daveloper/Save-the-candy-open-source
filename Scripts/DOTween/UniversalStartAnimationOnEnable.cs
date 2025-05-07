using UnityEngine;

public class UniversalStartAnimationOnEnable : MonoBehaviour
{
    /* Аткивирует выбранный DOTween аниматор автоматически */

    private UniversalAnimationMovement that;

    private void OnEnable()
    {
        that = GetComponent<UniversalAnimationMovement>();
        that.Show_And_Enable();
    }
}
