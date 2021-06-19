using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioExecutor : MonoBehaviour
{
    [SerializeField] private string _sfxName;

    private int _clipNumber;
    private AudioClip _clip;
    [SerializeField] private List<AudioClip> _clipList;
    private int _randomClip;


    private void SetAudioClip()
    {
        _randomClip = Random.Range(0, _clipList.Count);
        _clip = _clipList[_randomClip];
    }

    public AudioClip GetAudioClip()
    {
        SetAudioClip();
        return _clip;
    }

    public string GetAudioName()
    {
        return _sfxName;
    }
}
