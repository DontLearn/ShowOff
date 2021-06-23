using UnityEngine;
using Data;


public class LoadData : MonoBehaviour {
    [SerializeField]
    private bool _forceLoad = false;


    private void Start() {
        if ( _forceLoad ) {
            if ( !PersistentData.Instance.DataInstantiated ) {
                Debug.Log( $"{this}: Initializing data items.." );
                PersistentData.Instance.InstantiateData();
            }

            Debug.Log( $"{this}: Loading all data items.." );
            PersistentData.Instance.LoadAllPersistentItems();
        }
        else {
            if ( !PersistentData.Instance.DataInstantiated ) {
                Debug.Log( $"{this}: Initializing data items.." );
                PersistentData.Instance.InstantiateData();
            }
            else {
                Debug.Log( $"{this}: Loading all data items.." );
                PersistentData.Instance.LoadAllPersistentItems();
            }
        }
    }
}