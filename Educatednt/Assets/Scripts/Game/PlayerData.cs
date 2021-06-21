using System.Collections.Generic;
using UnityEngine;


namespace Data {
    public class PlayerData : PersistentDataBehaviour {
        [SerializeField]
        private Dictionary<string, int> datapairs = new Dictionary<string, int>() {
            { "health", 0 },
            { "upgrade", 0 },
            { "damage", 0 },
            { "knockback", 0 },
            { "jumpHeight", 0 }
        };



        public override void Load( PersistentData persistentData ) {
            Dictionary<string, int> newDic = new Dictionary<string, int>();

            foreach ( KeyValuePair<string, int> pair in datapairs ) {
                if ( int.TryParse( persistentData.GetStringData( pair.Key ), out int data ) ) {
                    newDic.Add( pair.Key, pair.Value );
                }
                else {
                    Debug.LogError( $"Could not parse {pair.Key} to an int." );
                }
            }

            datapairs = newDic;
        }


        public override void Save( PersistentData persistentData ) {
            foreach ( KeyValuePair<string, int> pair in datapairs ) {
                persistentData.SetIntData( pair.Key, pair.Value );
            }
        }
    }
}