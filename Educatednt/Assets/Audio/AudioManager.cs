using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioCarrier _audioCarrier;

    private enum Location {Menu, Castle, Level};

    [SerializeField]
    private Location _currentLocation;

    [SerializeField]
    private List<int> _sceneDefinition;

    void Awake()
    {
        _audioCarrier = GetComponent<AudioCarrier>();
    }

    private void Start()
    {
        DefineLocation();
        ChangeState();
    }

    private void ChangeState()
    {
        switch (_currentLocation)
        {
            case Location.Menu:
                
                break;
                _audioCarrier.PlaySound(0);
            case Location.Castle:
                _audioCarrier.PlaySound(1);
                _audioCarrier.PlaySound(2);
                _audioCarrier.PlaySound(3);
                break;

            case Location.Level:
                _audioCarrier.PlaySound(4);
                _audioCarrier.PlaySound(5);
                break;
        }
    }

    private void DefineLocation()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;

        if (_currentScene == _sceneDefinition[0])
        {
            _currentLocation = Location.Menu;
        }
        else if (_currentScene == _sceneDefinition[1])
        {
            _currentLocation = Location.Castle;
        }
        else if (_currentScene >= _sceneDefinition[2])
        {
            _currentLocation = Location.Level;
        }
    }
}
