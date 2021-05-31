using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private int _mainMenuSceneNumber;

    private bool _isActive;
    void Start()
    {
        _pauseMenu.SetActive(false);
        _isActive = false;
    }

    void Update()
    {
        _pauseMenu.SetActive(_isActive);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateMenu();
        }
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneNumber);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // SETTINGS menu functions & features
}
