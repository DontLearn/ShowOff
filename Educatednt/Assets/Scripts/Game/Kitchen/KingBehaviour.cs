using System.Collections.Generic;


namespace Data {
    public class KingBehaviour : PersistentDataBehaviour {
        public enum Data {
            HAPPINESS,
            BURGERS_EATEN,
        }

        protected Dictionary<Data, int> data = new Dictionary<Data, int>() {
            { Data.HAPPINESS, 0 },
            { Data.BURGERS_EATEN, 0 },
        };



        public override void Load( PersistentData persistentData ) {
            isLoaded = true;
        }


        public override void Save( PersistentData persistentData ) { }


        protected override void Upgrade() { }
    }
}