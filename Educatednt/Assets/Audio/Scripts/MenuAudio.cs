using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    [SerializeField] private AudioFileHandler _menuSounds;
    void Start()
    {
        _menuSounds = GetComponent<AudioFileHandler>();
        if (null == _menuSounds)
        {
            Destroy(this);
            return;
        }
        
        _menuSounds.PlaySound(4, true);
    }

    public void PauseMenuSound()
    {
        _menuSounds.PlaySound(0, false);
    }

    public void MenuClickSound()
    {
        _menuSounds.PlaySound(1, false);
    }

    public void BackButtonSound()
    {
        _menuSounds.PlaySound(2, false);
    }

    public void CookingButtonSound()
    {
        _menuSounds.PlaySound(3, false);
    }
}
