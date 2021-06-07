using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAudioSource : MonoBehaviour
{
    private AudioManager _manager;
    [SerializeField] private int _neededAudioSorce;
    private GameObject audioManagerObject;

    void Awake()
    {
        audioManagerObject = GameObject.Find("AudioManager");
        _manager = audioManagerObject.GetComponent<AudioManager>();
    }

    public void PlayAudio()
    {
        _manager.PlaySound(_manager.SetUpAudioSource(_neededAudioSorce));
    }

    public void StopAudio()
    {
        _manager.StopSound(_manager.SetUpAudioSource(_neededAudioSorce));
    }
}
