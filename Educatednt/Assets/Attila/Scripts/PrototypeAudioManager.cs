using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PrototypeAudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSources;

    private void Start()
    {
        PlayAmbientSound();
    }

    public void PlaySound(AudioSource _source)
    {
        _source.Play();
    }
    public void StopSound(AudioSource _source)
    {
        _source.Stop();
    }

    public AudioSource SetUpAudioSource(int _numberOfSource)
    {
        return _audioSources[_numberOfSource];
    }

    private void PlayAmbientSound()
    {
        PlaySound(_audioSources[0]);
    }
}
