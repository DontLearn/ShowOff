using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAudioSource : MonoBehaviour
{
    private PrototypeAudioManager _manager;
    [SerializeField] private int _neededAudioSorce;
    private GameObject audioManagerObject;

    void Awake()
    {
        audioManagerObject = GameObject.Find("AudioManager");
        _manager = audioManagerObject.GetComponent<PrototypeAudioManager>();


        /// temporary code: {
        if ( !_manager ) {
            Debug.LogWarning( "this script is dependent on an AudioManager in the scene. It's not there. Handle it" );
            enabled = false;
        }
        /// }
    }

    public void PlayAudio()
    {
        /// ERROR: Some kind of nullreference error. I think there wasn't an AudioManager in the scene
        /// TODO: Make sure that your code doesn't error when there is no manager in the scene,
        /// instead it should just not function or destroy itself. Code that errors when it can't find
        /// a specific object should let you know the object is not there and disable itself.
        /// 
        /// Check out other scripts, you can find other examples that use the Debug.Assert(); method,
        /// or look up the Debug.Assert(); method up in Unity's API online.
        if ( null != _manager )
            _manager.PlaySound(_manager.SetUpAudioSource(_neededAudioSorce));
    }

    public void StopAudio() {
        /// idem
        if ( null != _manager )
            _manager.StopSound(_manager.SetUpAudioSource(_neededAudioSorce));
    }
}