using UnityEngine;


namespace Data {
    public class KingBehaviour : PersistentDataBehaviour {
        protected int happiness = 0;
        private string _key = "happiness";

        protected int burgersEaten = 0;
        private string _keyTwo = "burgersEaten";

        protected bool isLoaded;

        public override void Load( PersistentData persistentData ) {

            if ( int.TryParse( persistentData.GetStringData( _key ), out int value ) ) {
                happiness = value;
            }
            else {
                Debug.LogError( $"Could not parse {_key} to an int." );
            }

            if (int.TryParse(persistentData.GetStringData(_keyTwo), out int valueTwo))
            {
                burgersEaten = valueTwo;
            }
            else
            {
                Debug.LogError($"Could not parse {_keyTwo} to an int.");
            }
        }

        public override void Save( PersistentData persistentData ) {
            persistentData.SetIntData( _key, happiness );
            persistentData.SetIntData(_keyTwo, burgersEaten);
        }
    }
}