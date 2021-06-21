using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioExecutor : MonoBehaviour
{
    [SerializeField] private string _sfxName;
    [SerializeField] private AudioSource _source;

    private AudioClip _clip;
    [SerializeField] private List<AudioClip> _clipList;
    private int _randomClip;


    public void SetAudioClip()
    {
        _randomClip = Random.Range(0, _clipList.Count);

        if(_clipList.Count != 0)
        {
            _clip = _clipList[_randomClip];
            _source.clip = _clip;
        }
        else
        {
            _source.clip = null;
        }
    }
    
    public AudioSource SetSource()
    {
        return _source;
    }
}
