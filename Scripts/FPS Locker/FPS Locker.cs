using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
