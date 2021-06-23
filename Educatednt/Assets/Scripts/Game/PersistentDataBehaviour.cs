using UnityEngine;


namespace Data {
    public abstract class PersistentDataBehaviour : MonoBehaviour {
        public abstract void Load( PersistentData persistentData );
        public abstract void Save( PersistentData persistentData );
        protected abstract void Upgrade();

        protected bool isLoaded = false;
        protected bool isUpgraded = false;



        private void Awake () {
            PersistentData.Instance.AddToPersistencyManager( this );
        }


        private void OnDestroy () {
            PersistentData.Instance.RemoveFromPersistencyManager( this );
        }
    }
}