using UnityEngine;


namespace Data {
    public abstract class PersistentDataBehaviour : MonoBehaviour {
        public abstract void Load ( PersistentData persistentData );
        public abstract void Save ( PersistentData persistentData );



        void Awake () {
            PersistentData.Instance.AddToPersistencyManager( this );
        }


        void OnDestroy () {
            PersistentData.Instance.RemoveFromPersistencyManager( this );
        }
    }
}