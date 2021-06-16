using System;
namespace GXPEngine
{
    public class King : PersistentDataBehaviour
    {
        string health;

        public King()
        {
        }
        
        public override void Load(PersistentData persistentData)
        {
            health = persistentData.GetStringData("updateState");
        }

        public override void Save(PersistentData persistentData)
        {
            persistentData.SetStringData("health", health);
        }

    }
}
