using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    /* Этот скрипт нужен для загрузки сцен. Также соблюдена инкапсуляция. */

    public void LoadSceneByIndex(int id)
    {
        SceneManager.LoadSceneAsync(id);
    }
}
