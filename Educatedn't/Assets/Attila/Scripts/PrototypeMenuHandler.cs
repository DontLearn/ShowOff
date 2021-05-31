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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        _audioMixer.SetFloat("MainVolume", _volumeSlider.value);
    }
}