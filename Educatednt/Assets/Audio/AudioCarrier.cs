using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioCarrier : MonoBehaviour
{
    private FMOD.Studio.EventInstance _eventInstance;
    [EventRef]
    [SerializeField]
    private List<string> fmodEvents;

    public void PlaySound(int _listElementNumber)
    {
        _eventInstance = RuntimeManager.CreateInstance(fmodEvents[_listElementNumber]);
        _eventInstance.start();
    }
}
