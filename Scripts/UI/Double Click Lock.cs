using UnityEngine;
using UnityEngine.UI;

public class DoubleClickLock : MonoBehaviour
{
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
