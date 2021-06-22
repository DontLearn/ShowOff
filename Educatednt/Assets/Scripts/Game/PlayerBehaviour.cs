using System.Collections.Generic;
using UnityEngine;


namespace Data {
    public class PlayerBehaviour : PersistentDataBehaviour {
        protected Dictionary<string, int> data = new Dictionary<string, int>() {
            { "health", 0 },
            { "upgrade", 0 },
            { "damage", 0 },
            { "knockback", 0 },
            { "jumpForce", 0 }
        };


        protected bool isLoaded = false;



        public override void Load( PersistentData persistentData ) {
            Dictionary<string, int> newDic = new Dictionary<string, int>();

            foreach ( KeyValuePair<string, int> pair in data ) {
                if ( int.TryParse( persistentData.GetStringData( pair.Key ), out int data ) ) {
                    newDic.Add( pair.Key, pair.Value );
                }
                else {
                    Debug.LogError( $"Could not parse {pair.Key} to an int." );
                }
            }

            data = newDic;
            isLoaded = true;
        }


        public override void Save( PersistentData persistentData ) {
            foreach ( KeyValuePair<string, int> pair in data ) {
                persistentData.SetIntData( pair.Key, pair.Value );
            }
        }
    }
}