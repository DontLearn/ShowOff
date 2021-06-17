using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTest : MonoBehaviour
{
    [SerializeField]
    private AudioCarrier _audioList;

    [SerializeField]
    private int _event;

    void Start()
    {
        _audioList = GetComponent<AudioCarrier>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioList.PlaySound(_event);
        }
    }
}
