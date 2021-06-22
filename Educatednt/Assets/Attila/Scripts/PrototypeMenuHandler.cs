using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PrototypeMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _volumeSlider;

    [SerializeField]
    private string _staringtLevel;

    private bool _isActive;

    void Start()
    {
        _menu.SetActive(false);
        _isActive = false;
    }

    void Update()
    {
        _menu.SetActive(_isActive);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateMenu();
        }

        SetVolume();
    }

    public void ActivateMenu()
    {
        _isActive = !_isActive;

        if (_isActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void RestartLevel()
    {
        if(_staringtLevel == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(_staringtLevel);
        }
        
		Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        /// ERROR: NullReferenceException. Audio mixer or Volume slider missing.
        /// TODO: Same thing. Not present? Then don't perform a function on them,
        /// instead provide debug information like this.
        if ( null != _audioMixer && null != _volumeSlider )
            _audioMixer.SetFloat("MainVolume", _volumeSlider.value);
        else
            Debug.LogError( $"{this}: either audio mixer or volume slider are missing." );
    }
}