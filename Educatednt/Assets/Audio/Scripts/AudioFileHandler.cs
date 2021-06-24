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


        if(_listPosition < _sfx.Count && _listPosition >= 0)
        {
            _executor = _sfx[_listPosition];
            if (_executor != null)
            {
                _executor.SetAudioClip();
                _source = _executor.SetSource();
                if (_source != null)
                {
                    _source.loop = _loop;
                    _source.Play();
                }

            }
        }
        
        
    }
}
