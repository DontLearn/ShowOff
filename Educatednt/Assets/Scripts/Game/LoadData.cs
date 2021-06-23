using UnityEngine;
using Data;


public class LoadData : MonoBehaviour {
    void Start() {
        if ( PersistentData.Instance.DataInstantiated ) {
            Debug.Log( $"{this}: Loading all data items.." );
            PersistentData.Instance.LoadAllPersistentItems();
        }
        else {
            Debug.Log( $"{this}: Initializing data items.." );
            PersistentData.Instance.InstantiateData();
        }
    }
}