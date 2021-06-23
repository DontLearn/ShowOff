using System.Collections.Generic;
using System;
using UnityEngine;


namespace Data {
    public class PlayerBehaviour : PersistentDataBehaviour {
        public enum Data {
            UPGRADE,
            HEALTH,
            DAMAGE,
            KNOCKBACK,
            JUMPFORCE
        }

        protected Dictionary<Data, int> data = new Dictionary<Data, int>() {
            { Data.UPGRADE, 2 },
            { Data.HEALTH, 100 },
            { Data.DAMAGE, 10 },
            { Data.KNOCKBACK, 70 },
            { Data.JUMPFORCE, 21 }
        };



        public override void Load( PersistentData persistentData ) {
            try {
                Dictionary<Data, int> newDic = new Dictionary<Data, int>();
                Debug.Log( $"{this}: Creating new dic.." );

                foreach ( KeyValuePair<Data, int> pair in data ) {
                    int dataInt;
                    if ( int.TryParse( persistentData.GetStringData( pair.Key.ToString() ), out dataInt ) ) {
                        newDic.Add( pair.Key, dataInt );
                        Debug.Log( $"{this}: Adding key {pair.Key} with value {dataInt} to new dic.." );
                    }
                    else {
                        Debug.LogError( $"{this}: Could not parse {pair.Key} to an int." );
                    }
                }

                data = newDic;
                Debug.Log( $"{this}: Overwriting dic.." );
            }
            catch( Exception e ) {
                Debug.LogError( $"{this}: Load failed with error {e.Message}." );
            }
            finally {
                isLoaded = true;
            }
        }


        public override void Save( PersistentData persistentData ) {
            foreach ( KeyValuePair<Data, int> pair in data ) {
                persistentData.SetIntData( pair.Key.ToString(), pair.Value );
            }
        }


        protected override void Upgrade() {
            isUpgraded = true;
        }
    }
}