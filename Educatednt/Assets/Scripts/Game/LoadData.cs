using UnityEngine;
using Data;


public class LoadData : MonoBehaviour
{
    void Start() {
        /// TEST
        /*Debug.Log( $"{this}: Test: Saving all data items.." );
        PersistentData.Instance.SaveAllPersistentItems();*/
        ///


        Debug.Log( $"{this}: Loading all data items.." );
        PersistentData.Instance.LoadAllPersistentItems();
    }
}
