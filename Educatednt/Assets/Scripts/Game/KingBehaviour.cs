using UnityEngine;


namespace Data {
    public class KingBehaviour : PersistentDataBehaviour {
        protected int happiness = 0;
        protected bool isLoaded;
        private string _key = "happiness";



        public override void Load( PersistentData persistentData ) {
            if ( int.TryParse( persistentData.GetStringData( _key ), out int value ) ) {
                happiness = value;
            }
            else {
                Debug.LogError( $"Could not parse {_key} to an int." );
            }
        }

        public override void Save( PersistentData persistentData ) {
            persistentData.SetIntData( _key, happiness );
        }
    }
}