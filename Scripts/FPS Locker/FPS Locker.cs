using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    /* Ограничивает FPS на сценах, где присутствует этот скрипт (У меня на всех) */

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
