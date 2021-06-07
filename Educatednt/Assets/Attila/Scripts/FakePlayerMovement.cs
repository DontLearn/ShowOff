using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayAudioSource _jumpSound;
    [SerializeField] private PlayAudioSource _run;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpSound.PlayAudio();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            _run.PlayAudio();
        } else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _run.StopAudio();
        }
    }
}
