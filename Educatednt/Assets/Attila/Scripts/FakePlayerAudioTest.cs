using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerAudioTest : MonoBehaviour
{
    [SerializeField] private AudioFileHandler _sound;

    void Update()
    {
        FakePlayerJump();
    }

    private void FakePlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sound.PlaySound(0, false);
        }
    }
}
