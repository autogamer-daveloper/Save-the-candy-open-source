using UnityEngine;

public class EnableOther : MonoBehaviour
{
    /* Какая-то чушь, но отвечающая за включение другого объекта, если этот включен и наоборот. */

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
