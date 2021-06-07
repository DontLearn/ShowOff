using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextLanguage : MonoBehaviour
{
    [SerializeField]
    private Text _thisText;

    [SerializeField]
    private MainMenuHandler _mainMenu;

    [SerializeField]
    List<string> _textOptions;

    
    void Update()
    {
        VisualizeText();
    }

    private void VisualizeText()
    {
        int _currentLanguage = _mainMenu.GetLanguageValue();
        _thisText.text = _textOptions[_currentLanguage];
    }
}
