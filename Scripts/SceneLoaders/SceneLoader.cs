using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadSceneByIndex(int id)
    {
        SceneManager.LoadSceneAsync(id);
    }
}
