using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioFileHandler : MonoBehaviour
{
    [SerializeField] private List<AudioExecutor> _sfx;
    private List<string> _sfxNames;
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        for(int i = 0; i < _sfx.Count; i++)
        {
            _sfxNames[i] = _sfx[i].GetAudioName();
        }
    }

    public void PlaySound(string _name, bool _loop)
    {
        AudioExecutor _executor;
        
        int i;
        for (i = 0; i < _sfxNames.Count; i++)
        {
            if (_name == _sfxNames[i])
            {
                PassIntValue(i);
                _executor = _sfx[i];
                _source.clip = _executor.GetAudioClip();
                _source.Play();
                _source.loop = _loop;
            }
        }
    }

    public void PlaySound(int _listPosition, bool _loop)
    {
        AudioExecutor _executor;

        _executor = _sfx[_listPosition];
        _source.clip = _executor.GetAudioClip();
        _source.Play();
        _source.loop = _loop;
    }

    private int PassIntValue(int _passedValue)
    {
        return _passedValue;
    }

    public void StopSound()
    {
        _source.Stop();
    }
}
