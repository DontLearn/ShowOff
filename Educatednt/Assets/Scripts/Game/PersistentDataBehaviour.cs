using System;
namespace GXPEngine
{
    public abstract class PersistentDataBehaviour
    {
        public PersistentDataBehaviour()
        {
        }

        public abstract void Load(PersistentData persistentData);
        public abstract void Save(PersistentData persistentData);

        void Awake()
        {
            PersistentData.instance.AddToPersistencyManager(this);
        }

        void OnDestroy()
        {
            PersistentData.instance.RemoveFromPersistencyManager(this);
        }
    }
}
