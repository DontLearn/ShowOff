using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerAudioTest : MonoBehaviour
{
    [SerializeField] private AudioFileHandler _sound;

    void Update()
    {
        FakePlayerJump();
        FakePlayerRun();
    }

    private void FakePlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sound.PlaySound("Jump", false);
        }
    }

    private void FakePlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _sound.PlaySound("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _sound.StopSound();
        }
    }
}
