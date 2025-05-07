using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TranslateEasyIntegration
{
    [AddComponentMenu ("Translate/Select Language Script", 0)]
    public class SelectLanguage : MonoBehaviour
    {
        [Header ("Language Settings")]
        [SerializeField]private string[] keys;
        [SerializeField]private int sceneId;
        [SerializeField]private bool useBeutifulReloader;
        [SerializeField]private GameObject sceneReLoader;
        [SerializeField]private float sceneReLoaderTime = 0.5f;

        private UniversalAnimationMovement anim;

        private void Start()
        {
            anim = sceneReLoader.GetComponent<UniversalAnimationMovement>();
        }

        public void Select_Language(int LangId)
        {
            for(int i = 0; i < keys.Length; i++)
            {
                PlayerPrefs.DeleteKey(keys[i]);
            }

            PlayerPrefs.SetString(keys[LangId], "selected language");
            
            if(useBeutifulReloader)
            {
                sceneReLoader.SetActive(true);
                anim.Show_And_Enable();
                Invoke("ReloadScene", sceneReLoaderTime);
            }
            else
            {
                SceneManager.LoadScene(sceneId);
            }
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}