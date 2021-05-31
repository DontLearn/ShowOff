using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private int _gameSceneNumber;
    [SerializeField]
    private Dropdown _resolutionDropdown;
    [SerializeField]
    private int _languageValue;

    Resolution[] _availableResolutions;

    private void Start()
    {
        _availableResolutions = Screen.resolutions;
        SetupResolution();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneNumber);
    }

    public void Exit()
    {
        Application.Quit();
    }


    // SETTINGS
    public void SetLanguage(int _language)
    {
        _languageValue = _language; 
    }

    public int GetLanguageValue()
    {
        return _languageValue;
    }
    private void SetupResolution()
    {
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int _currentResolutionIndex = 0;
        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            string option = _availableResolutions[i].width + " x " + _availableResolutions[i].height;
            options.Add(option);

            if (_availableResolutions[i].width == Screen.currentResolution.width && _availableResolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int _resolutionInt)
    {
        Resolution _resolution = _availableResolutions[_resolutionInt];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool _isFullScreen)
    {
        Screen.fullScreen = _isFullScreen;
    }

    public void SetQuality (int _qualityInt)
    {
        QualitySettings.SetQualityLevel(_qualityInt);
    }

    // AUDIO settings
}