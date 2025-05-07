using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TranslateEasyIntegration
{
    /* Это мой немного изменённый скрипт из одного гайда на перевод игры на ютубе. @A-GAMES-STUDIO */

    [AddComponentMenu("Translate/TextMeshPro", 2)]
    public class TranslateTMP : MonoBehaviour
    {
        private TMP_Text _textComponent;

        [Header ("Language settings")]
        [SerializeField]private string[] lang;
        [SerializeField]private string[] texts;

        private void Start()
        {
            _textComponent = GetComponent<TMP_Text>();

            int selectedLangIndex = -1;

            for (int i = 0; i < lang.Length; i++)
            {
                if (PlayerPrefs.HasKey(lang[i]))
                {
                    selectedLangIndex = i;
                    break;
                }
            }

            if (selectedLangIndex == -1)
            {
                selectedLangIndex = 0;
                PlayerPrefs.SetString(lang[0], "selected language");
            }

            SelectLanguageText(selectedLangIndex);
        }

        private void SelectLanguageText(int LangId)
        {
            _textComponent.text = texts[LangId];
        }
    }
}