using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbientSoundPlayer : MonoBehaviour
{
    [SerializeField] private string _kitchenSceneName;

    [SerializeField] private AudioFileHandler _ambientAudioFiles;


    void Start()
    {
        if(_kitchenSceneName == SceneManager.GetActiveScene().name)
        {
            _ambientAudioFiles.PlaySound(1, true);
        }
        else
        {
            _ambientAudioFiles.PlaySound(0, true);
        }
    }
}
