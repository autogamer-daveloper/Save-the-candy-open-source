using UnityEngine;
using UnityEngine.UI;

public class DoubleClickLock : MonoBehaviour
{
    /* Защита от дабл-клика на кнопке, на которую вы повесили этот скрипт. */

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Deactivating);
    }

    private void Deactivating()
    {
        button.interactable = false;
    }
}
