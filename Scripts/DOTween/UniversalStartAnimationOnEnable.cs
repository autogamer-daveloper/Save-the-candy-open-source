using UnityEngine;

public class UniversalStartAnimationOnEnable : MonoBehaviour
{
    private UniversalAnimationMovement that;

    private void OnEnable()
    {
        that = GetComponent<UniversalAnimationMovement>();
        that.Show_And_Enable();
    }
}
