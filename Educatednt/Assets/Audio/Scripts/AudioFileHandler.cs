using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioFileHandler : MonoBehaviour
{
    [SerializeField] private List<AudioExecutor> _sfx;

    public void PlaySound(int _listPosition, bool _loop)
    {
        AudioExecutor _executor;
        AudioSource _source;

        _executor = _sfx[_listPosition];
        _executor.SetAudioClip();
        _source = _executor.SetSource();
        _source.loop = _loop;
        _source.Play();
    }
}
