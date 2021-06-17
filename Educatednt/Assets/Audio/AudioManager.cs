using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;


namespace Audio {
    public class AudioManager : MonoBehaviour {
        private enum Location {
            MENU,
            CASTLE,
            LEVEL
        };


        [SerializeField]
        private AudioCarrier _audioCarrier;

        [SerializeField]
        private Location _currentLocation;

        [SerializeField]
        private List<int> _sceneDefinition;




        private void Awake() {
            _audioCarrier = GetComponent<AudioCarrier>();
        }


        private void Start() {
            DefineLocation();
            ChangeState();
        }


        private void ChangeState() {
            switch (_currentLocation)
            {
                case Location.MENU:
                    _audioCarrier.PlaySound(0);
                    break;
                case Location.CASTLE:
                    _audioCarrier.PlaySound(1);
                    _audioCarrier.PlaySound(2);
                    _audioCarrier.PlaySound(3);
                    break;
                case Location.LEVEL:
                    _audioCarrier.PlaySound(4);
                    _audioCarrier.PlaySound(5);
                    break;
            }
        }


        private void DefineLocation() {
            _currentLocation = ( Location )SceneManager.GetActiveScene().buildIndex;
        }
    }
}