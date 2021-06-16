using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerWalkingSound : MonoBehaviour
{
    [EventRef]
    [SerializeField]
    private string _playerWalkingSound;

    [SerializeField, Range (0.1f, 1f)]
    private float _walkingSpeed;
    private bool _isPlayerMoving;

    void Start()
    {
        InvokeRepeating("PlayRunSound", 0, _walkingSpeed);
    }

    public void PlayRunSound()
    {
        if (_isPlayerMoving)
        {
            RuntimeManager.PlayOneShot(_playerWalkingSound);
        }
    }

    public bool MovementDetection(bool _playerMovement)
    {
        return _playerMovement;
        _isPlayerMoving = _playerMovement;
    }
}
