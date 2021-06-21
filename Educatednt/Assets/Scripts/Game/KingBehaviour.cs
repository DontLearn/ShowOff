using UnityEngine;


namespace Data {
    public class KingBehaviour : PersistentDataBehaviour {
        [SerializeField]
        private string key = "happiness";
        
        private int happiness = 0;
        


        public override void Load( PersistentData persistentData ) {
            if ( int.TryParse( persistentData.GetStringData( key ), out int value ) ) {
                happiness = value;
            }
            else {
                Debug.LogError( $"Could not parse {key} to an int." );
            }
        }

        public override void Save( PersistentData persistentData ) {
            persistentData.SetIntData( key, happiness );
        }
    }
}