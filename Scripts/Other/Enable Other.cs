using UnityEngine;

public class EnableOther : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;

    private void OnEnable()
    {
        foreach(GameObject _obj in obj)
        {
            _obj.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach(GameObject _obj in obj)
        {
            _obj.SetActive(false);
        }
    }
}
