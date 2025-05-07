using UnityEngine;

public class ThisLevelId : MonoBehaviour
{
    /* Кладёт в PlayerPrefsStorgare информацию о уровне, который ты проходишь. */

    [Range (1, 5)] // Вообще я хотел 10 уровней, но как то лень без собственного гейм-дизайнера и 3д художника
    [SerializeField] private int id;

    private void Start()
    {
        var _strogare = new PlayerPrefsStorgare();
        _strogare.This_Level(id);
    }
}
